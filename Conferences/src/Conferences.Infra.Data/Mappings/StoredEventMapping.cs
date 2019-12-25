using Conferences.Domain.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conferences.Infra.Data.Mappings
{
    public class StoredEventMapping : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.Property(p => p.Timestamp)
                .HasColumnName("CreationDate");

            builder.Property(p => p.MessageType)
                .HasColumnName("Action")
                .HasColumnType("varchar(100)");

            builder.Property(c => c.User)
                .HasColumnType("varchar(300)");
        }
    }
}
