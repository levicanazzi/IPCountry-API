using MediatR;
using IpCountry.Application.IPAdresses.CommandSide.QuerySide;
using IpCountry.Application.IPAdresses.CommandSide.Repositories;
using IpCountry.Domain.Notifications;
using IpCountry.Infrastructure.Core.Commands;
using IpCountry.Infrastructure.Core.Repositories;

namespace IpCountry.Application.IPAdresses.CommandSide.Commands.GenerateReports
{
    public class GenerateIPReportCommandHandler : BaseCommandHandler, IRequestHandler<GenerateIPReportCommand, IEnumerable<IPReportCountryViewModel>>
    {
        private readonly IIPAddressRepository _iPAddressRepository;
        public GenerateIPReportCommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<DomainNotification> notifications, IIPAddressRepository iPAddressRepository) : base(uow, bus, notifications)
        {
            _iPAddressRepository = iPAddressRepository;
        }

        public async Task<IEnumerable<IPReportCountryViewModel>> Handle(GenerateIPReportCommand request, CancellationToken cancellationToken)
        {
            var result = await _iPAddressRepository.IPReportCountry(request.TwoLetterCodes);

            if (result is null)
            {
                return Error<IEnumerable<IPReportCountryViewModel>>("No item found");
            }

            return Success<IEnumerable<IPReportCountryViewModel>>(result);
        }
    }
}
