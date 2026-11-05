using FluentValidation;

namespace IpCountry.Application.IPAdresses.CommandSide.Commands.GenerateReports
{
    public class GenerateIPReportCommandValidator : AbstractValidator<GenerateIPReportCommand>
    {
        public GenerateIPReportCommandValidator()
        {
        }
    }
}
