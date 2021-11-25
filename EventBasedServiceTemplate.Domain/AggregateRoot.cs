using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace EventBasedServiceTemplate.Domain
{
    public abstract class AggregateRoot
    {
        private readonly Dictionary<string, Action<DomainEvent>> _registeredEventHandlers;

        private readonly static JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        protected AggregateRoot()
        {
            _registeredEventHandlers = new Dictionary<string, Action<DomainEvent>>();

            RegisterEventHandlers();
        }

        protected abstract void RegisterEventHandlers();

        protected void RegisterEventHandler<TEventData>(Action<DomainEventWithData<TEventData>> handler) 
            where TEventData : class
        {
            var key = typeof(TEventData).Name;

            if (!_registeredEventHandlers.ContainsKey(key))
            {
                _registeredEventHandlers.Add(key, domainEvent => handler(PrepareAfterFetch<TEventData>(domainEvent)));
            }
        }

        private static DomainEventWithData<TEventData> PrepareAfterFetch<TEventData>(DomainEvent domainEvent)
            where TEventData : class
        {
            return new DomainEventWithData<TEventData>
            {
                Id = domainEvent.Id,
                Type = domainEvent.Type,
                AggregateId = domainEvent.AggregateId,
                AggregateName = domainEvent.AggregateName,
                OccurredAt = domainEvent.OccurredAt,
                RawEventData = domainEvent.RawEventData,
                Data = JsonSerializer.Deserialize<TEventData>(domainEvent.RawEventData, JsonSerializerOptions)
            };
        }

        public void ApplyEvents(IEnumerable<DomainEvent> events)
        {
            foreach (var validEvent in events.Where(ev => _registeredEventHandlers.ContainsKey(ev.Type)))
            {
                _registeredEventHandlers[validEvent.Type](validEvent);
            }
        }
    }
}
