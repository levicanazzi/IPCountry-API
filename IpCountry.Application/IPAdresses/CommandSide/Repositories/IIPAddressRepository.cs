using IpCountry.Application.IPAdresses.CommandSide.Aggregates;
using IpCountry.Application.IPAdresses.CommandSide.QuerySide;
using IpCountry.Infrastructure.Core.Repositories;

namespace IpCountry.Application.IPAdresses.CommandSide.Repositories
{
    public interface IIPAddressRepository : IRepository<IPAddress>
    {
        ValueTask UpdateAsync(IPAddress ipAddress);
        ValueTask<IPAddress> FindByIPAsync(string ipAddress);
        ValueTask<IEnumerable<IPAddress>> FindAllAsync();
        ValueTask<IEnumerable<IPReportCountryViewModel>> IPReportCountry(IEnumerable<string>? twoLetterCodes);
    }
}
