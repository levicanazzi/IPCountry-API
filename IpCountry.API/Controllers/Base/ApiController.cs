using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IpCountry.Domain.Notifications;

namespace IpCountry.API.Controllers.Base
{
    public abstract class ApiController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediator _mediator;

        protected ApiController(INotificationHandler<DomainNotification> notifications,
                                IMediator mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }

        protected async ValueTask<IActionResult> Dispatch<TResponse>(IRequest<TResponse> request)
        {
            return Result(await _mediator.Send(request));
        }
        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.Publish(new DomainNotification(code, message));
        }

        protected IActionResult Result<TResponse>(TResponse result)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected async ValueTask<IActionResult> Dispatch<TResponse>(Func<ValueTask<TResponse>> func)
            => Result(await func.Invoke());


        protected async ValueTask<IActionResult> Dispatch<TResponse>(ValueTask<Result<TResponse>> result)
        {
            var response = await result;

            if (response.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = new List<string> { response.Error }
                });
            }

            return Result(response.Value);
        }

        protected IActionResult InvalidRequest(string message)
        {
            return BadRequest(new
            {
                success = false,
                errors = new List<string> { message }
            });
        }
    }
}