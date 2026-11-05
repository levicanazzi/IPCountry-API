namespace IpCountry.Infrastructure.Core.Repositories
{
    public interface IRepository<TAggregate>
    {
        ValueTask<TAggregate> FindAsync(int id);
    }
}
