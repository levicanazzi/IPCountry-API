using FluentValidation;
using MediatR;
using IpCountry.Domain.Notifications;

namespace IpCountry.Infrastructure.Core.Mediatr
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IMediator _mediator;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IMediator mediator)
        {
            _validators = validators;
            _mediator = mediator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .Select(error => new DomainNotification(error.ErrorCode, error.ErrorMessage));

            if (!failures.Any())
                return await next().ConfigureAwait(false);


            foreach (var failure in failures)
                await _mediator.Publish(failure);

            return await Task.FromResult(default(TResponse));
        }
    }
}