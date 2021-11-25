using EventBasedServiceTemplate.Domain.Example.AggregateRoots;

namespace EventBasedServiceTemplate.Domain.Example.Events
{
    public class ExampleEvent<TEventData> : DomainEventWithData<TEventData>
        where TEventData : class
    {
        public ExampleEvent()
        {
            AggregateName = nameof(ExampleAggregate);
        }
    }
}
