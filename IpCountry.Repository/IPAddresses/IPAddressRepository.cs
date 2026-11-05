using Microsoft.EntityFrameworkCore;
using IpCountry.Application.IPAdresses.CommandSide.Aggregates;
using IpCountry.Application.IPAdresses.CommandSide.QuerySide;
using IpCountry.Application.IPAdresses.CommandSide.Repositories;
using IpCountry.Repository.Context;
using IpCountry.Repository.Extensions;
using System.Text;

namespace IpCountry.Repository.IPAddresses
{
    public class IPAddressRepository : IIPAddressRepository
    {
        private readonly IpCountryDbContext _context;
        public IPAddressRepository(IpCountryDbContext context)
        {
            _context = context;
        }

        public async ValueTask<IPAddress> FindAsync(int id)
        {
            return await _context.IPAddresses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async ValueTask<IPAddress> FindByIPAsync(string ipAddress)
        {
            return await _context.IPAddresses.FirstOrDefaultAsync(c => c.IP == ipAddress);
        }

        public async ValueTask UpdateAsync(IPAddress ipAddress)
        {
            _context.IPAddresses.Attach(ipAddress);

            await Task.CompletedTask;
        }
        public async ValueTask<IEnumerable<IPAddress>> FindAllAsync()
        {
            return await _context.IPAddresses.ToListAsync();
        }

        public async ValueTask<IEnumerable<IPReportCountryViewModel>> IPReportCountry(IEnumerable<string>? twoLetterCodes)
        {
            var parameters = new Dictionary<string, object>();

            var queryBuilder = new StringBuilder(@"SELECT 
                                                        c.Name AS CountryName, 
                                                        COUNT(i.Id) AS AddressesCount, 
                                                        MAX(i.UpdatedAt) AS LastAddressUpdated
                                                    FROM 
                                                        Countries c 
                                                    INNER JOIN 
                                                        IPAddresses i ON i.CountryId = c.Id ");

            if (twoLetterCodes != null && twoLetterCodes.Any(code => !string.IsNullOrEmpty(code)))
            {
                queryBuilder.Append("WHERE ");

                var validCodes = twoLetterCodes.Where(code => !string.IsNullOrEmpty(code)).ToList();
                QueryBuilder.AppendListToQueryBuilder(queryBuilder, validCodes, "c.TwoLetterCode", parameters);
            }

            queryBuilder.Append(@"GROUP BY 
                                    c.Name
                                ORDER BY 
                                    AddressesCount DESC ");

            var result = await _context.SelectFromRawSqlAsync<IPReportCountryViewModel>(queryBuilder.ToString(), parameters);

            return result;
        }
    }
}
