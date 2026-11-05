using CSharpFunctionalExtensions;
using MediatR;
using IpCountry.Domain.Notifications;
using IpCountry.Infrastructure.Core.Repositories;

namespace IpCountry.Infrastructure.Core.Commands
{
    public class BaseCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _bus;
        private readonly DomainNotificationHandler _notifications;

        public BaseCommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<DomainNotification> notifications)
        {
            _unitOfWork = uow;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<bool> Commit()
        {
            if (_notifications.HasNotifications()) return false;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExecuteInTrasactionAsync(Func<Task> action)
        {
            return await _unitOfWork.ExecuteInTrasactionAsync(action);
        }

        public async Task<Result> ExecuteInTrasactionAsync(Func<Task<Result>> action)
        {
            return await _unitOfWork.ExecuteInTrasactionAsync(action);
        }

        public Unit Error(string value)
        {
            _bus.Publish(new DomainNotification("error", value));
            return Unit.Value;
        }

        public T Error<T>(string value)
        {
            _bus.Publish(new DomainNotification("error", value));
            return default;
        }

        public static Unit Success() => Unit.Value;

        public static T Success<T>(object value) => (T)value;
        public static T Success<T>() => default;

        public bool HasNotifications() => _notifications.HasNotifications();

        public IEnumerable<string> GetNotifications() => _notifications.GetNotifications().Select(n => n.Value);
    }
}
