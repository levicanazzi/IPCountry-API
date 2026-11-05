using Microsoft.EntityFrameworkCore;
using IpCountry.Application.Countries.CommandSide.Aggregates;
using IpCountry.Application.Countries.CommandSide.QuerySide;
using IpCountry.Application.Countries.CommandSide.Repositories;
using IpCountry.Repository.Context;

namespace IpCountry.Repository.Countries
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IpCountryDbContext _context;
        public CountryRepository(IpCountryDbContext context)
        {
            _context = context;
        }

        public async ValueTask InsertAsync(Country country)
        {
            await _context.Countries.AddAsync(country);

            await Task.CompletedTask;
        }

        public async ValueTask UpdateAsync(Country country)
        {
            _context.Countries.Attach(country);

            await Task.CompletedTask;
        }

        public async ValueTask<CountryViewModel> FindByIPAsync(string ip)
        {
            var result = await (from country in _context.Countries
                                join ipAddresses in _context.IPAddresses on country.Id equals ipAddresses.CountryId
                                where ipAddresses.IP == ip
                                select new CountryViewModel
                                {
                                    CountryName = country.Name,
                                    TwoLetterCode = country.TwoLetterCode,
                                    ThreeLetterCode = country.ThreeLetterCode
                                }
                                ).FirstOrDefaultAsync();

            return result;
        }

        public async ValueTask<Country> FindByNameAsync(string name, string twoLetterCode, string threeLetterCode)
        {
            return await _context.Countries
                                .FirstOrDefaultAsync(c => c.Name == name &&
                                                    c.TwoLetterCode == twoLetterCode &&
                                                    c.ThreeLetterCode == threeLetterCode);
        }

        public async ValueTask<Country> FindAsync(int id)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
