using FluentValidation;

namespace IpCountry.Application.IPAdresses.CommandSide.Commands.UpSert
{
    public class UpsertIPCommandValidator : AbstractValidator<UpsertIPCommand>
    {
        public UpsertIPCommandValidator()
        {
        }
    }
}
