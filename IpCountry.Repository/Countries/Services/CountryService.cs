using CSharpFunctionalExtensions;
using IpCountry.Application.Countries.CommandSide.Aggregates;
using IpCountry.Application.Countries.CommandSide.Repositories;
using IpCountry.Application.Countries.CommandSide.Services;
using IpCountry.Application.IPAdresses.CommandSide.Aggregates;

namespace IpCountry.Repository.Countries.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async ValueTask<Result<Country>> Integration(IPAddress ipAddress)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"https://ip2c.org/{ipAddress.IP}");

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<Country>("Erro to access de API");
            }

            var content = await response.Content.ReadAsStringAsync();

            var parts = content.Split(';');

            var newCountry = Country.Create(parts[3], parts[1], parts[2]);

            if (newCountry.IsFailure)
            {
                return Result.Failure<Country>(newCountry.Error);
            }

            return Result.Success<Country>(newCountry.Value);
        }
    }
}