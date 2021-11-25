using System;

namespace EventBasedServiceTemplate.Domain
{
    public interface IDomainCommand<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public TKey AggregateId { get; }
    }
}
