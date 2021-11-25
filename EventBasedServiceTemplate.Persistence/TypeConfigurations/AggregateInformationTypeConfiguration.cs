
using EventBasedServiceTemplate.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBasedServiceTemplate.Persistence.TypeConfigurations
{
    public class AggregateInformationTypeConfiguration : IEntityTypeConfiguration<AggregateInformation>
    {
        public void Configure(EntityTypeBuilder<AggregateInformation> builder)
        {
            builder
                .ToTable("aggregates")
                .HasKey(agg => new { agg.Id, agg.Type })
                .HasName("pk_aggregates");

            builder
                .Property(ai => ai.Id)
                .HasColumnName(nameof(AggregateInformation.Id).ToLower());

            builder
                .Property(ai => ai.Type)
                .HasColumnName(nameof(AggregateInformation.Type).ToLower());

            builder
                .Property(ai => ai.Version)
                .HasColumnName(nameof(AggregateInformation.Version).ToLower());
        }
    }
}
