using CSharpFunctionalExtensions;

namespace IpCountry.Infrastructure.Core.Handlers
{
    public interface IHandler<TRequest>
    {
        ValueTask<Result> Handle(TRequest request);
    }
}
