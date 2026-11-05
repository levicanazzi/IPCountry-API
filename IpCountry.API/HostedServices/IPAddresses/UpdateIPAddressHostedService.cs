using Microsoft.Extensions.Options;
using IpCountry.Application.IPAdresses.CommandSide.Commands.UpSert;
using IpCountry.Infrastructure.Core.HostedServices;

namespace IpCountry.API.HostedServices.IPAddresses
{
    public class UpdateIPAddressHostedService : RequestServiceBase<UpsertIPCommand>
    {
        public UpdateIPAddressHostedService(IServiceProvider serviceProvider, ILogger<HostedServiceBase> logger, IOptions<HostedServiceSettings> settings)
        : base(serviceProvider, logger, settings)
        {
        }
    }
}
