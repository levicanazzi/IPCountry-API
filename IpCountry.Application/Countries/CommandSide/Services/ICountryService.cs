using CSharpFunctionalExtensions;
using IpCountry.Application.Countries.CommandSide.Aggregates;
using IpCountry.Application.IPAdresses.CommandSide.Aggregates;

namespace IpCountry.Application.Countries.CommandSide.Services
{
    public interface ICountryService
    {
        ValueTask<Result<Country>> Integration(IPAddress ipAddress);
    }
}
