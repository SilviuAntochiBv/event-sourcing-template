namespace EventBasedServiceTemplate.Domain
{
    public interface IDomainEventHandler<TEventData>
        where TEventData : class
    {
        void Apply(DomainEventWithData<TEventData> domainEvent);
    }
}
