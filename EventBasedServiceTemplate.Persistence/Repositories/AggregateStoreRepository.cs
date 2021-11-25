using System.Threading;
using System.Threading.Tasks;

using EventBasedServiceTemplate.Domain;
using EventBasedServiceTemplate.Domain.Contracts.Persistence;

namespace EventBasedServiceTemplate.Persistence.Repositories
{
    public class AggregateStoreRepository : IAggregateStoreRepository
    {
        private readonly AppDbContext _dbContext;

        public AggregateStoreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task StoreAggregate(AggregateInformation aggregateInformation, CancellationToken cancellationToken = default)
        {
            await _dbContext.Aggregates.AddAsync(aggregateInformation, cancellationToken);
        }
    }
}
