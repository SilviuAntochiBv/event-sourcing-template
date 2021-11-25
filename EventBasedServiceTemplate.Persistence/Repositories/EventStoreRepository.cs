using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EventBasedServiceTemplate.Domain;
using EventBasedServiceTemplate.Domain.Contracts.Persistence;

namespace EventBasedServiceTemplate.Persistence.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly AppDbContext _dbContext;

        public EventStoreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateEventStore(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            var storeDomainEvents = domainEvents.Select(de => new DomainEvent
            {
                AggregateId = de.AggregateId,
                AggregateName = de.AggregateName,
                Id = de.Id,
                OccurredAt = de.OccurredAt,
                RawEventData = de.RawEventData,
                Type = de.Type
            });

            await _dbContext.Events.AddRangeAsync(storeDomainEvents, cancellationToken);
        }
    }
}
