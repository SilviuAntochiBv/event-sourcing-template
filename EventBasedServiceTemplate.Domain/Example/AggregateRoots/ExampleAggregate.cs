using System.Collections.Generic;
using System.Linq;

using EventBasedServiceTemplate.Domain.Example.Entities;
using EventBasedServiceTemplate.Domain.Example.Events.Incoming;
using EventBasedServiceTemplate.Domain.Example.Events.Outgoing;
using EventBasedServiceTemplate.Domain.Example.ValueObjects;

namespace EventBasedServiceTemplate.Domain.Example.AggregateRoots
{
    public class ExampleAggregate :
        AggregateRoot,
        IDomainEventHandler<ExampleAddedEventData>,
        IDomainEventHandler<ExampleNameChangedEventData>,
        IDomainEventHandler<ExampleValueChangedEventData>,
        IDomainEventHandler<ExampleAddressAddedEventData>,
        IDomainEventHandler<ExampleAddressDeletedEventData>,
        IDomainEventHandler<SubExampleAddedEventData>,
        IDomainEventHandler<SubExampleUpdatedEventData>,
        IDomainEventHandler<SubExampleDeletedEventData>,
        IDomainEventHandler<ExampleProcessedEventData>
    {
        private readonly List<SubExampleEntity> _subExamples;
        private readonly HashSet<ExampleAddress> _addresses;

        public string ParentName { get; private set; }
        public int ParentValue { get; private set; }
        public string ProcessingStatus { get; private set; }

        public IReadOnlyCollection<SubExampleEntity> SubExamples => _subExamples.AsReadOnly();

        public IReadOnlyCollection<ExampleAddress> Addresses => _addresses.ToList().AsReadOnly();

        public ExampleAggregate()
        {
            _subExamples = new List<SubExampleEntity>();
            _addresses = new HashSet<ExampleAddress>();
        }

        #region Event Handlers
        protected override void RegisterEventHandlers()
        {
            // Outgoing events
            RegisterEventHandler<ExampleAddedEventData>(Apply);
            RegisterEventHandler<ExampleNameChangedEventData>(Apply);
            RegisterEventHandler<ExampleValueChangedEventData>(Apply);
            RegisterEventHandler<ExampleAddressAddedEventData>(Apply);
            RegisterEventHandler<ExampleAddressDeletedEventData>(Apply);
            RegisterEventHandler<SubExampleAddedEventData>(Apply);
            RegisterEventHandler<SubExampleUpdatedEventData>(Apply);
            RegisterEventHandler<SubExampleDeletedEventData>(Apply);

            // Incoming events
            RegisterEventHandler<ExampleProcessedEventData>(Apply);
        }

        public void Apply(DomainEventWithData<ExampleAddedEventData> domainEvent)
        {
            ParentName = domainEvent.Data.Name;
            ParentValue = domainEvent.Data.Value;
        }

        public void Apply(DomainEventWithData<ExampleNameChangedEventData> domainEvent)
        {
            ParentName = domainEvent.Data.Name;
        }

        public void Apply(DomainEventWithData<ExampleValueChangedEventData> domainEvent)
        {
            ParentValue = domainEvent.Data.Value;
        }

        public void Apply(DomainEventWithData<ExampleAddressAddedEventData> domainEvent)
        {
            _addresses.Add(new ExampleAddress(domainEvent.Data.StreetName, domainEvent.Data.Town));
        }

        public void Apply(DomainEventWithData<ExampleAddressDeletedEventData> domainEvent)
        {
            _addresses.Remove(new ExampleAddress(domainEvent.Data.StreetName, domainEvent.Data.Town));
        }

        public void Apply(DomainEventWithData<SubExampleAddedEventData> domainEvent)
        {
            _subExamples.Add(new SubExampleEntity
            {
                Id = domainEvent.Data.Id,
                ChildDescription = domainEvent.Data.Description
            });
        }

        public void Apply(DomainEventWithData<SubExampleUpdatedEventData> domainEvent)
        {
            var correspondingSubExample = _subExamples.FirstOrDefault(subExample => subExample.Id == domainEvent.Data.Id);

            if (correspondingSubExample != null)
            {
                correspondingSubExample.ChildDescription = domainEvent.Data.Description;
            }
        }

        public void Apply(DomainEventWithData<SubExampleDeletedEventData> domainEvent)
        {
            var correspondingSubExample = _subExamples.FirstOrDefault(subExample => subExample.Id == domainEvent.Data.Id);

            if (correspondingSubExample != null)
            {
                _subExamples.Remove(correspondingSubExample);
            }
        }

        public void Apply(DomainEventWithData<ExampleProcessedEventData> domainEvent)
        {
            ProcessingStatus = domainEvent.Data.ProcessingStatus;
        }
        #endregion
    }
}
