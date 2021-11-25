using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventBasedServiceTemplate.Domain.Contracts.Persistence
{
    public interface IAggregateQueryRepository<TAggregate, TKey> 
        where TAggregate : AggregateRoot
        where TKey : struct, IEquatable<TKey>
    {
        Task<IEnumerable<TAggregate>> GetAll(string aggregateName, CancellationToken cancellationToken = default);

        Task<TAggregate> GetById(string aggregateName, TKey aggregateId, CancellationToken cancellationToken = default);
    }
}
