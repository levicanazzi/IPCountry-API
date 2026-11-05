using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IpCountry.Infrastructure.Core.HostedServices
{
    public abstract class HostedServiceBase : BackgroundService
    {
        protected readonly ILogger<HostedServiceBase> _logger;
        protected readonly IServiceProvider _serviceProvider;

        protected HostedServiceBase(IServiceProvider serviceProvider, ILogger<HostedServiceBase> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker {Name} started", GetType().Name);

            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker {Name} finished", GetType().Name);

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

            await base.StopAsync(cancellationToken);
        }
    }
}