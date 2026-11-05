using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IpCountry.Application.Countries.CommandSide.Aggregates;

namespace IpCountry.Repository.Mappings.Countries
{
    public class CountryTbMap : IEntityTypeConfiguration<Country>
    {
        private readonly string _tabelaNome = "Countries";

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable(_tabelaNome);

            builder.HasKey(t => t.Id)
                   .HasName($"PK_{_tabelaNome}");

            builder.Property(t => t.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                   .HasMaxLength(100)  // THIS IS BECAUSE THERE ARE COUNTRY NAMES WITH MORE THAN 50 CHARACTERS
                   .HasColumnType("varchar(100)")
                   .IsRequired();

            builder.Property(t => t.TwoLetterCode)
                   .HasMaxLength(2)
                   .HasColumnType("char(2)")
                   .IsRequired();

            builder.Property(t => t.ThreeLetterCode)
                   .HasMaxLength(3)
                   .HasColumnType("char(3)")
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("getutcdate()");
        }
    }

}
