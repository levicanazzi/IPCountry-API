namespace IpCountry.Application.IPAdresses.CommandSide.QuerySide
{
    public class IPReportCountryViewModel
    {
        public string? CountryName { get; set; }
        public int AddressesCount { get; set; }
        public DateTime LastAddressUpdated { get; set; }
    }
}
