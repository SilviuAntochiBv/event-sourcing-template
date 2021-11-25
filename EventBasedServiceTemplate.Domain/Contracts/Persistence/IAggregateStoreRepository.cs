using System.Threading;
using System.Threading.Tasks;

namespace EventBasedServiceTemplate.Domain.Contracts.Persistence
{
    public interface IAggregateStoreRepository
    {
        Task StoreAggregate(AggregateInformation aggregateInformation, CancellationToken cancellationToken = default);
    }
}
