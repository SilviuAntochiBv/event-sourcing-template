using System;

namespace EventBasedServiceTemplate.Domain
{
    public class CommandMetadata<TCommand, TKey> where TKey : struct, IEquatable<TKey>
    {
        public TKey AggregateId { get; }

        public TCommand Command { get; }

        public CommandMetadata(TKey aggregateId, TCommand command)
        {
            AggregateId = aggregateId;

            Command = command;
        }
    }
}
