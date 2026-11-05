using CSharpFunctionalExtensions;

namespace IpCountry.Infrastructure.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> ExecuteInTrasactionAsync(Func<Task> action);
        Task<Result> ExecuteInTrasactionAsync(Func<Task<Result>> action);
    }
}