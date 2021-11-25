using EventBasedServiceTemplate.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBasedServiceTemplate.Persistence.TypeConfigurations
{
    public class DomainEventTypeConfiguration : IEntityTypeConfiguration<DomainEvent>
    {
        public void Configure(EntityTypeBuilder<DomainEvent> builder)
        {
            builder.ToTable("events");

            builder
                .Property(de => de.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName(nameof(DomainEvent.Id).ToLower());

            builder
                .Property(de => de.Type)
                .HasColumnName(nameof(DomainEvent.Type).ToLower());

            builder
                .Property(de => de.AggregateName)
                .HasColumnName("aggregate_name");

            builder
                .Property(de => de.AggregateId)
                .HasColumnName("aggregate_id");

            builder
                .Property(de => de.OccurredAt)
                .HasColumnName("occurred_at");

            builder
                .Property(de => de.RawEventData)
                .HasColumnName("raw_event_data")
                .HasColumnType("json");

            builder.HasKey(de => de.Id).HasName("pk_events");
        }
    }
}
