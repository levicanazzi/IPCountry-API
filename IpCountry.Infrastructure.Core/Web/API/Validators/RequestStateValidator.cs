using FluentValidation.Results;
using IpCountry.Infrastructure.Core.Models;

namespace IpCountry.Infrastructure.Core.Web.API.Validators
{
    public class RequestStateValidator
    {
        private readonly List<ValidationFailure> _validationFailures;

        public RequestStateValidator()
        {
            _validationFailures = new List<ValidationFailure>();
        }

        public void Add(ValidationFailure validationFailure)
        {
            _validationFailures.Add(validationFailure);
        }

        public void Add(IEnumerable<ValidationFailure> validationFailures)
        {
            _validationFailures.AddRange(validationFailures);
        }

        public bool IsValid()
        {
            return !_validationFailures.Any();
        }

        public IReadOnlyList<ValidationFailure> Failures => _validationFailures.AsReadOnly();

        public IEnumerable<Error> Errors => _validationFailures.Select(x => new Error(x.PropertyName, x.ErrorMessage));
    }
}