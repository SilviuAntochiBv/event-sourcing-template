
using System.Diagnostics.CodeAnalysis;

using EventBasedServiceTemplate.Domain;
using EventBasedServiceTemplate.Persistence.TypeConfigurations;

using Microsoft.EntityFrameworkCore;

namespace EventBasedServiceTemplate.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<DomainEvent> Events { get; set; }

        public DbSet<AggregateInformation> Aggregates { get; set; }

        public AppDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DomainEventTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AggregateInformationTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
