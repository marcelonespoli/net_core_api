using Conferences.Domain.Conferences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conferences.Infra.Data.Mappings
{
    public class ConferenceMapping : IEntityTypeConfiguration<Conference>
    {
        public void Configure(EntityTypeBuilder<Conference> builder)
        {
            builder.Property(e => e.Name)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(e => e.ShortDescription)
                .HasColumnType("varchar(150)");

            builder.Property(e => e.LongDescription)
                .HasColumnType("varchar(max)");

            builder.Property(e => e.CompanyName)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Ignore(e => e.Tags);
            builder.Ignore(e => e.ValidationResult);            
            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Conferences");

            builder.HasOne(p => p.Organizer)
                .WithMany(p => p.Conferences)
                .HasForeignKey(f => f.OrganizerId);

            builder.HasOne(p => p.Category)
                .WithMany(p => p.Conferences)
                .HasForeignKey(f => f.CategoryId)
                .IsRequired(false);
        }
    }
}
