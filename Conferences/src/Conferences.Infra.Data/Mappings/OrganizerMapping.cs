using Conferences.Domain.Organizers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conferences.Infra.Data.Mappings
{
    public class OrganizerMapping : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(e => e.Email)
               .HasColumnType("varchar(100)")
               .IsRequired();

            builder.Property(e => e.DocumentId)
               .HasColumnType("varchar(30)")
               .HasMaxLength(30)
               .IsRequired();

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Organizers");
        }
    }
}
