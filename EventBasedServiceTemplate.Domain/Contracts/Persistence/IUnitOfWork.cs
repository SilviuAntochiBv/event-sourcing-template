using System.Threading;
using System.Threading.Tasks;

namespace EventBasedServiceTemplate.Domain.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IAggregateStoreRepository AggregateStoreRepository { get; }

        IEventStoreRepository EventStoreRepository { get; }

        Task Commit(CancellationToken cancellationToken = default);
    }
}
