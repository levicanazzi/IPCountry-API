using IpCountry.Application.Countries.CommandSide.Aggregates;
using IpCountry.Application.Countries.CommandSide.QuerySide;
using IpCountry.Infrastructure.Core.Repositories;

namespace IpCountry.Application.Countries.CommandSide.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        ValueTask<CountryViewModel> FindByIPAsync(string ip);
        ValueTask<Country> FindByNameAsync(string name, string twoLetterCode, string threeLetterCode);
        ValueTask InsertAsync(Country country);
        ValueTask UpdateAsync(Country country);
    }
}
