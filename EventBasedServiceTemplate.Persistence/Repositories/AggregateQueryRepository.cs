using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EventBasedServiceTemplate.Domain;
using EventBasedServiceTemplate.Domain.Contracts.Persistence;

using Microsoft.EntityFrameworkCore;

namespace EventBasedServiceTemplate.Persistence.Repositories
{
    public class AggregateQueryRepository<TAggregate, TKey> : IAggregateQueryRepository<TAggregate, TKey>
        where TAggregate : AggregateRoot, new()
        where TKey : struct, IEquatable<TKey>
    {
        private readonly AppDbContext _dbContext;

        public AggregateQueryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TAggregate>> GetAll(string aggregateName, CancellationToken cancellationToken = default)
        {
            var aggregateEvents = await GetEventsForAggregate(aggregateName).ToListAsync(cancellationToken);

            return aggregateEvents.GroupBy(de => de.AggregateId).Select(grouping => CreateAggregate(grouping.ToList()));
        }

        public async Task<TAggregate> GetById(string aggregateName, TKey aggregateId, CancellationToken cancellationToken = default)
        {
            var aggregateEvents = await GetEventsForAggregate(aggregateName).Where(de => de.AggregateId == aggregateId.ToString()).ToListAsync(cancellationToken);

            return CreateAggregate(aggregateEvents);
        }

        private IQueryable<DomainEvent> GetEventsForAggregate(string aggregateName)
        {
            return _dbContext.Events.Where(de => de.AggregateName == aggregateName).OrderBy(de => de.Id);
        }

        private static TAggregate CreateAggregate(IEnumerable<DomainEvent> aggregateEvents)
        {
            var result = new TAggregate();

            result.ApplyEvents(aggregateEvents);

            return result;
        }
    }
}
