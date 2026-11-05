using MediatR;
using Microsoft.AspNetCore.Mvc;
using IpCountry.API.Controllers.Base;
using IpCountry.Application.Countries.CommandSide.Commands.Find;
using IpCountry.Application.Countries.CommandSide.Repositories;
using IpCountry.Application.IPAdresses.CommandSide.Commands.GenerateReports;
using IpCountry.Domain.Notifications;
using IpCountry.Repository.Context;

namespace IpCountry.API.Controllers.v1.CountriesController
{
    [Route("v1/countries")]
    [ApiController]
    public class CountryController : ApiController
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IpCountryDbContext _context;
        public CountryController(INotificationHandler<DomainNotification> notifications, IMediator mediator, ICountryRepository countryRepository, IpCountryDbContext context) : base(notifications, mediator)
        {
            _countryRepository = countryRepository;
            _context = context;
        }

        [HttpGet]
        [Route("{ip}")]
        public async ValueTask<IActionResult> FindByIp([FromRoute] FindCountryCommand request)
        {
            return await Dispatch(request);
        }

        [HttpPost]
        [Route("report")]
        public async ValueTask<IActionResult> Report([FromBody] GenerateIPReportCommand request)
        {
            return await Dispatch(request);
        }
    }
}
