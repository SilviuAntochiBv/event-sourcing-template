using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventBasedServiceTemplate.Domain.Contracts.Persistence
{
    public interface IEventStoreRepository
    {
        Task UpdateEventStore(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
