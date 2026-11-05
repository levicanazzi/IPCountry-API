using CSharpFunctionalExtensions;

namespace IpCountry.Infrastructure.Core.Models
{
    public sealed class Error : ValueObject
    {
        private const string Separator = "||";

        public string Code { get; }
        public string Message { get; }

        internal Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public static Error Create(string code, string message)
        {
            return new Error(code, message);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Code;
        }

        public string Serialize()
        {
            return $"{Code}{Separator}{Message}";
        }

        public static Error Deserialize(string serialized)
        {
            if (serialized == "A non-empty request body is required.")
                return Errors.General.ValueIsRequired();

            string[] data = serialized.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);

            return data.Length < 2 ? throw new Exception($"Invalid error serialization: '{serialized}'") : new Error(data[0], data[1]);
        }
    }
}