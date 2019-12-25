using Conferences.Domain.Conferences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conferences.Infra.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Ignore(p => p.ValidationResult);
            builder.Ignore(p => p.CascadeMode);

            builder.ToTable("Categories");
        }
    }
}
