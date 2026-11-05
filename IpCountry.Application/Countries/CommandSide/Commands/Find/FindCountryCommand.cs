using MediatR;
using IpCountry.Application.Countries.CommandSide.QuerySide;

namespace IpCountry.Application.Countries.CommandSide.Commands.Find
{
    public class FindCountryCommand : IRequest<CountryViewModel>
    {
        public string IP { get; set; }
    }
}

