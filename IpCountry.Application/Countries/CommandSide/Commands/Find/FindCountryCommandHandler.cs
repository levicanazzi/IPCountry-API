using MediatR;
using Microsoft.Extensions.Caching.Memory;
using IpCountry.Application.Countries.CommandSide.QuerySide;
using IpCountry.Application.Countries.CommandSide.Repositories;
using IpCountry.Domain.Notifications;
using IpCountry.Infrastructure.Core.Commands;
using IpCountry.Infrastructure.Core.Repositories;

namespace IpCountry.Application.Countries.CommandSide.Commands.Find
{
    public class FindCountryCommandHandler : BaseCommandHandler, IRequestHandler<FindCountryCommand, CountryViewModel>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMemoryCache _memoryCache;
        public FindCountryCommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<DomainNotification> notifications, ICountryRepository countryRepository, IMemoryCache memoryCache) : base(uow, bus, notifications)
        {
            _countryRepository = countryRepository;
            _memoryCache = memoryCache;
        }

        public async Task<CountryViewModel> Handle(FindCountryCommand request, CancellationToken cancellationToken)
        {
            string cacheKey = $"country_{request.IP}";

            if (!_memoryCache.TryGetValue(cacheKey, out var country))
            {
                country = await _countryRepository.FindByIPAsync(request.IP);

                if (country is null)
                {
                    return Error<CountryViewModel>("Country not found");
                }
                _memoryCache.Set(cacheKey, country);

                return Success<CountryViewModel>(country);
            }

            return Success<CountryViewModel>(country);
        }
    }
}
