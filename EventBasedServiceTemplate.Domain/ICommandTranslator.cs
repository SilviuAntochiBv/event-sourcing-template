using System;
using System.Collections.Generic;

namespace EventBasedServiceTemplate.Domain
{
    public interface ICommandTranslator<TCommand, TAggregateKey> 
        where TAggregateKey : struct, IEquatable<TAggregateKey>
    {
        IEnumerable<DomainEvent> Process(CommandMetadata<TCommand, TAggregateKey> command);
    }
}
