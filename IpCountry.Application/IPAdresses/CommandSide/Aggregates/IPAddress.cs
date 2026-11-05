using CSharpFunctionalExtensions;

namespace IpCountry.Application.IPAdresses.CommandSide.Aggregates
{
    public class IPAddress : Entity<int>
    {
        protected IPAddress() { }
        private IPAddress(int IPAddressId, string iP, DateTime createdAt, DateTime updatedAt)
        {
            IPAddressId = IPAddressId;
            IP = iP;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public int CountryId { get; set; }
        public string IP { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static Result<IPAddress> Create(int countryId, string ip)
        {
            if (countryId <= 0)
            {
                return Result.Failure<IPAddress>("Enter a country id");
            }

            if (string.IsNullOrEmpty(ip))
            {
                return Result.Failure<IPAddress>("Enter an ip");
            }

            if (ip.Length > 15)
            {
                return Result.Failure<IPAddress>("The IP address has more than 15 characters");
            }

            var result = new IPAddress(countryId, ip, DateTime.Now, DateTime.Now);

            return Result.Success(result);
        }

        public Result Update(int countryId, string ip)
        {
            if (countryId <= 0)
            {
                return Result.Failure<IPAddress>("Enter a country id");
            }

            if (string.IsNullOrEmpty(ip))
            {
                return Result.Failure<IPAddress>("Enter an ip");
            }

            if (ip.Length > 15)
            {
                return Result.Failure<IPAddress>("The IP address has more than 15 characters");
            }

            CountryId = countryId;
            IP = ip;
            UpdatedAt = DateTime.Now;

            return Result.Success();
        }
    }
}
