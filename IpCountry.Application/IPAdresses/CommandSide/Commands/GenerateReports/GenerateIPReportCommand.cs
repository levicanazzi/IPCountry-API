using MediatR;
using IpCountry.Application.IPAdresses.CommandSide.QuerySide;

namespace IpCountry.Application.IPAdresses.CommandSide.Commands.GenerateReports
{
    public class GenerateIPReportCommand : IRequest<IEnumerable<IPReportCountryViewModel>>
    {
        public List<string>? TwoLetterCodes { get; set; } = new List<string>();
    }
}
