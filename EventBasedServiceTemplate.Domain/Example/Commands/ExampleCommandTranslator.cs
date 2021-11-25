using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using EventBasedServiceTemplate.Domain.Example.AggregateRoots;
using EventBasedServiceTemplate.Domain.Example.Events;
using EventBasedServiceTemplate.Domain.Example.Events.Outgoing;

namespace EventBasedServiceTemplate.Domain.Example.Commands
{
    public class ExampleCommandTranslator : IExampleCommandTranslator
    {
        private readonly static JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public IEnumerable<DomainEvent> Process(CommandMetadata<ExampleAddCommand, Guid> commandWithMetadata)
        {
            var result = new List<DomainEvent>
            {
                CreateEvent(
                    new ExampleAddedEventData(commandWithMetadata.Command.Name, commandWithMetadata.Command.Value), 
                    commandWithMetadata.AggregateId)
            };

            if (commandWithMetadata.Command.Addresses != null)
            {
                result.AddRange(commandWithMetadata.Command.Addresses.SelectMany(addAddressCommand => Process(new CommandMetadata<ExampleAddressAddCommand, Guid>(commandWithMetadata.AggregateId, addAddressCommand))));
            }

            if (commandWithMetadata.Command.SubExamples != null)
            {
                result.AddRange(commandWithMetadata.Command.SubExamples.SelectMany(addSubExampleCommand => Process(new CommandMetadata<SubExampleAddCommand, Guid>(commandWithMetadata.AggregateId, addSubExampleCommand))));
            }

            return result;
        }

        public IEnumerable<DomainEvent> Process(CommandMetadata<ExampleUpdateCommand, Guid> commandWithMetadata)
        {
            var result = new List<DomainEvent>();

            if (commandWithMetadata.Command.NewName != null)
            {
                result.Add(CreateEvent(
                    new ExampleNameChangedEventData(commandWithMetadata.Command.NewName), 
                    commandWithMetadata.AggregateId));
            }

            if (commandWithMetadata.Command.NewValue != null)
            {
                result.Add(CreateEvent(
                    new ExampleValueChangedEventData(commandWithMetadata.Command.NewValue.Value), 
                    commandWithMetadata.AggregateId));
            }

            return result;
        }

        public IEnumerable<DomainEvent> Process(CommandMetadata<ExampleAddressAddCommand, Guid> commandWithMetadata)
        {
            return new List<DomainEvent>
            {
                CreateEvent(
                    new ExampleAddressAddedEventData(commandWithMetadata.Command.NewStreetName, commandWithMetadata.Command.NewTown), 
                    commandWithMetadata.AggregateId)
            };
        }

        public IEnumerable<DomainEvent> Process(CommandMetadata<ExampleAddressDeleteCommand, Guid> commandWithMetadata)
        {
            return new List<DomainEvent>
            {
                CreateEvent(
                    new ExampleAddressDeletedEventData(commandWithMetadata.Command.ExistingStreetName, commandWithMetadata.Command.ExistingTown), 
                    commandWithMetadata.AggregateId)
            };
        }

        public IEnumerable<DomainEvent> Process(CommandMetadata<SubExampleAddCommand, Guid> commandWithMetadata)
        {
            return new List<DomainEvent>
            {
                CreateEvent(
                    new SubExampleAddedEventData(commandWithMetadata.Command.Id, commandWithMetadata.Command.NewDescription), 
                    commandWithMetadata.AggregateId)
            };
        }

        public IEnumerable<DomainEvent> Process(CommandMetadata<SubExampleUpdateCommand, Guid> commandWithMetadata)
        {
            return new List<DomainEvent>
            {
                CreateEvent(
                    new SubExampleUpdatedEventData(commandWithMetadata.Command.Id, commandWithMetadata.Command.NewDescription), 
                    commandWithMetadata.AggregateId)
            };
        }

        public IEnumerable<DomainEvent> Process(CommandMetadata<SubExampleDeleteCommand, Guid> commandWithMetadata)
        {
            return new List<DomainEvent>
            {
                CreateEvent(
                    new SubExampleDeletedEventData(commandWithMetadata.Command.Id), 
                    commandWithMetadata.AggregateId)
            };
        }

        private static ExampleEvent<TEventData> CreateEvent<TEventData>(TEventData data, Guid aggregateId)
            where TEventData : class
        {
            return new ExampleEvent<TEventData>
            {
                AggregateId = aggregateId.ToString(),
                AggregateName = typeof(ExampleAggregate).Name,
                Data = data,
                RawEventData = JsonSerializer.Serialize(data, JsonSerializerOptions)
            };
        }
    }
}
