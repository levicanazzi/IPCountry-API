using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using IpCountry.Application.Countries.CommandSide.Repositories;
using IpCountry.Application.Countries.CommandSide.Services;
using IpCountry.Application.IPAdresses.CommandSide.Repositories;
using IpCountry.Domain.Notifications;
using IpCountry.Infrastructure.Core.Commands;
using IpCountry.Infrastructure.Core.Models;
using IpCountry.Infrastructure.Core.Repositories;

namespace IpCountry.Application.IPAdresses.CommandSide.Commands.UpSert
{
    public class UpsertIPCommandHandler : BaseCommandHandler, IRequestHandler<UpsertIPCommand, Result<Unit, Error>>
    {
        private readonly ICountryService _countryService;
        private readonly IIPAddressRepository _iPAddressRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMemoryCache _memoryCache;
        public UpsertIPCommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<DomainNotification> notifications, ICountryService countryService, IIPAddressRepository iPAddressRepository, ICountryRepository countryRepository, IMemoryCache memoryCache) : base(uow, bus, notifications)
        {
            _countryService = countryService;
            _iPAddressRepository = iPAddressRepository;
            _countryRepository = countryRepository;
            _memoryCache = memoryCache;
        }

        public async Task<Result<Unit, Error>> Handle(UpsertIPCommand request, CancellationToken cancellationToken)
        {
            var ipAddresses = await _iPAddressRepository.FindAllAsync();

            if (!ipAddresses.Any())
            {
                return Error("Ip Address is not found");
            }

            const int batchSize = 100;

            var totalBatches = (int)Math.Ceiling((double)ipAddresses.Count() / batchSize);


            for (int batchIndex = 0; batchIndex < totalBatches; batchIndex++)
            {
                var batch = ipAddresses.Skip(batchIndex * batchSize).Take(batchSize);

                foreach (var ipAddress in batch)
                {
                    var result = await _countryService.Integration(ipAddress);

                    if (result.IsFailure)
                    {
                        return Error(result.Error);
                    }

                    var country = await _countryRepository.FindAsync(ipAddress.CountryId);

                    if (country is null)
                    {
                        return Error("Country is not found");
                    }

                    string cacheKey = $"country_{ipAddress.IP}";

                    if (country.Name == result.Value.Name)
                    {
                        ipAddress.Update(country.Id, ipAddress.IP);

                        await _iPAddressRepository.UpdateAsync(ipAddress);
                    }
                    else
                    {
                        _memoryCache.Remove(cacheKey);

                        var countryExistent = await _countryRepository.FindByNameAsync(result.Value.Name, result.Value.TwoLetterCode, result.Value.ThreeLetterCode);

                        if (countryExistent is null)
                        {
                            await _countryRepository.InsertAsync(result.Value);

                            await Commit();

                            var includedCountry = await _countryRepository.FindByNameAsync(result.Value.Name, result.Value.TwoLetterCode, result.Value.ThreeLetterCode);

                            ipAddress.Update(includedCountry.Id, ipAddress.IP);

                            await _iPAddressRepository.UpdateAsync(ipAddress);
                        }
                        else
                        {
                            ipAddress.Update(countryExistent.Id, ipAddress.IP);

                            await _iPAddressRepository.UpdateAsync(ipAddress);
                        }
                    }

                    await Commit();
                }
            }

            return Result.Success<Unit, Error>(Unit.Value);
        }
    }
}
