using FluentValidation;

namespace IpCountry.Application.Countries.CommandSide.Commands.Find
{
    public class FindCountryCommandValidator : AbstractValidator<FindCountryCommand>
    {
        public FindCountryCommandValidator()
        {
            RuleFor(c => c.IP)
                .NotEmpty()
                .WithMessage("Enter an IP")
                .MaximumLength(15)
                .WithMessage("The IP must have a maximum of 15 characters");
        }
    }
}
