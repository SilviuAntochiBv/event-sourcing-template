using System.Threading;
using System.Threading.Tasks;

using EventBasedServiceTemplate.Domain.Contracts.Persistence;

namespace EventBasedServiceTemplate.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IAggregateStoreRepository _aggregateStoreRepository;

        public IEventStoreRepository EventStoreRepository => _eventStoreRepository;
        public IAggregateStoreRepository AggregateStoreRepository => _aggregateStoreRepository;

        public UnitOfWork(
            AppDbContext dbContext,
            IEventStoreRepository eventStoreRepository,
            IAggregateStoreRepository aggregateStoreRepository)
        {
            _dbContext = dbContext;
            _eventStoreRepository = eventStoreRepository;
            _aggregateStoreRepository = aggregateStoreRepository;
        }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
