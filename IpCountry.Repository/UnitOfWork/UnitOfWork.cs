using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using IpCountry.Infrastructure.Core.Repositories;
using IpCountry.Repository.Context;

namespace IpCountry.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IpCountryDbContext _context;
        public UnitOfWork(IpCountryDbContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<bool> ExecuteInTrasactionAsync(Func<Task> action)
        {
            // Use of an EF Core resiliency strategy when using multiple DbContexts
            // within an explicit BeginTransaction():
            // https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteInTransactionAsync(async () =>
            {
                await action();

                await _context.SaveChangesAsync(false);

            }, async () => await Task.FromResult(true));

            return true;
        }

        public async Task<Result> ExecuteInTrasactionAsync(Func<Task<Result>> action)
        {
            var result = Result.Success();
            // Use of an EF Core resiliency strategy when using multiple DbContexts
            // within an explicit BeginTransaction():
            // https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();

                result = await action();

                if (result.IsSuccess)
                {
                    await _context.SaveChangesAsync(false);

                    await transaction.CommitAsync();
                }

            });

            return result;
        }

        public async ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
