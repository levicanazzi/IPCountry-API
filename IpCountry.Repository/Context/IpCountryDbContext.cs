using Dapper;
using Microsoft.EntityFrameworkCore;
using IpCountry.Application.Countries.CommandSide.Aggregates;
using IpCountry.Application.IPAdresses.CommandSide.Aggregates;
using IpCountry.Repository.Mappings.Countries;
using IpCountry.Repository.Mappings.IPAddresses;
using System.Dynamic;

namespace IpCountry.Repository.Context
{
    public class IpCountryDbContext : DbContext
    {
        public IpCountryDbContext(DbContextOptions<IpCountryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<IPAddress> IPAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CountryTbMap());
            modelBuilder.ApplyConfiguration(new IPAddressesTbMap());

            base.OnModelCreating(modelBuilder);
        }

        public async ValueTask<IEnumerable<TResult>> SelectFromRawSqlAsync<TResult>(string sql, Dictionary<string, object> parameters)
        where TResult : class
        {
            var param = new ExpandoObject() as IDictionary<string, object>;

            parameters.ToList().ForEach(x => param.Add(x.Key, x.Value));
            return await Database.GetDbConnection().QueryAsync<TResult>(sql, parameters);
        }
    }
}