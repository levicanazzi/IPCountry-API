namespace IpCountry.Infrastructure.Core.HostedServices
{
    public class HostedServiceSettings
    {
        public string CronExpression { get; set; } = "0 0 * * * *";
    }
}
