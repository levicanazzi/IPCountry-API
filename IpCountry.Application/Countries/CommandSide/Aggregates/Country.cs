using CSharpFunctionalExtensions;

namespace IpCountry.Application.Countries.CommandSide.Aggregates
{
    public class Country : Entity<int>
    {
        protected Country() { }
        private Country(string name, string twoLetterCode, string threeLetterCode, DateTime createdAt)
        {
            Name = name;
            TwoLetterCode = twoLetterCode;
            ThreeLetterCode = threeLetterCode;
            CreatedAt = createdAt;
        }

        public string Name { get; private set; } = null!;
        public string TwoLetterCode { get; private set; } = null!;
        public string ThreeLetterCode { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        public static Result<Country> Create(string name, string twoLetterCode, string threeLetterCode)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<Country>("Enter a name");
            }

            if (name.Length > 100)
            {
                return Result.Failure<Country>("The name has more than 100 characters.");
            }

            if (string.IsNullOrEmpty(twoLetterCode))
            {
                return Result.Failure<Country>("Enter a two letter code");
            }

            if (string.IsNullOrEmpty(threeLetterCode))
            {
                return Result.Failure<Country>("Enter a three letter code");
            }

            var result = new Country(name, twoLetterCode, threeLetterCode, DateTime.Now);

            return Result.Success(result);
        }

        public Result Update(string name, string twoLetterCode, string threeLetterCode)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<Country>("Enter a name");
            }

            if (string.IsNullOrEmpty(twoLetterCode))
            {
                return Result.Failure<Country>("Enter a two letter code");
            }

            if (string.IsNullOrEmpty(threeLetterCode))
            {
                return Result.Failure<Country>("Enter a three letter code");
            }

            Name = name;
            TwoLetterCode = twoLetterCode;
            ThreeLetterCode = threeLetterCode;
            CreatedAt = DateTime.Now;

            return Result.Success();
        }
    }
}