using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IpCountry.Application.Countries.CommandSide.Aggregates;
using IpCountry.Application.IPAdresses.CommandSide.Aggregates;

namespace IpCountry.Repository.Mappings.IPAddresses
{
    public class IPAddressesTbMap : IEntityTypeConfiguration<IPAddress>
    {
        private readonly string _tabelaNome = "IPAddresses";
        public void Configure(EntityTypeBuilder<IPAddress> builder)
        {
            builder.ToTable(_tabelaNome);

            builder.HasKey(t => t.Id)
                   .HasName($"PK_{_tabelaNome}");

            builder.Property(t => t.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.CountryId)
                   .IsRequired();

            builder.Property(t => t.IP)
                   .HasMaxLength(15)
                   .HasColumnType("char(15)")
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("getutcdate()");

            builder.Property(t => t.UpdatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("getutcdate()");

            builder.HasIndex(t => t.IP)
                   .IsUnique()
                   .HasDatabaseName("IX_IPAddresses");

            builder.HasOne<Country>()
                   .WithMany()
                   .HasForeignKey(t => t.CountryId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_IPAddresses_Countries");
        }
    }
}
