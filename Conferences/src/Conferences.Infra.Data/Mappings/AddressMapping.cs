using Conferences.Domain.Conferences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conferences.Infra.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(p => p.Address1)
                .HasMaxLength(150)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(e => e.Address2)
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            builder.Property(e => e.Address3)
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            builder.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");            

            builder.Property(e => e.Postcode)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(e => e.County)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Ignore(p => p.ValidationResult);
            builder.Ignore(p => p.CascadeMode);

            builder.ToTable("Addresses");

            builder.HasOne(p => p.Conference)
                .WithOne(p => p.Address)
                .HasForeignKey<Address>(k => k.ConferenceId)
                .IsRequired(false);
        }
    }
}
