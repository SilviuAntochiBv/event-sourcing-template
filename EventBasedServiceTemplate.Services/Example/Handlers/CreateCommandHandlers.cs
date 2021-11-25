using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EventBasedServiceTemplate.Domain;
using EventBasedServiceTemplate.Domain.Contracts.Persistence;
using EventBasedServiceTemplate.Domain.Example.AggregateRoots;
using EventBasedServiceTemplate.Domain.Example.Commands;
using EventBasedServiceTemplate.Domain.Example.Events;
using EventBasedServiceTemplate.Domain.Example.Events.Outgoing;

using MediatR;

using QueueClient.Contracts;

namespace EventBasedServiceTemplate.Behavior.Example.Handlers
{
    public class CreateCommandHandlers : BaseCommandHandler,
        IRequestHandler<ExampleAddCommand, Guid>,
        IRequestHandler<ExampleAddressAddCommand>,
        IRequestHandler<SubExampleAddCommand, long>
    {
        private readonly IUniqueIdGenerator _uniqueIdGenerator;
        private readonly IExampleCommandTranslator _exampleCommandTranslator;

        public CreateCommandHandlers(
            IUniqueIdGenerator uniqueIdGenerator,
            IExampleCommandTranslator exampleCommandTranslator,
            IUnitOfWork unitOfWork,
            IMediator mediator, 
            IMessagePublisher messagePublisher)
            : base(unitOfWork, mediator, messagePublisher)
        {
            _uniqueIdGenerator = uniqueIdGenerator;
            _exampleCommandTranslator = exampleCommandTranslator;
        }

        public async Task<Guid> Handle(ExampleAddCommand request, CancellationToken cancellationToken)
        {
            var aggregateId = _uniqueIdGenerator.GenerateUuid();

            var commandWithMetadata = new CommandMetadata<ExampleAddCommand, Guid>(aggregateId, request);

            var generatedEvents = _exampleCommandTranslator.Process(commandWithMetadata);

            try
            {
                await UnitOfWork.EventStoreRepository.UpdateEventStore(generatedEvents, cancellationToken);

                await UnitOfWork.AggregateStoreRepository.StoreAggregate(new AggregateInformation
                {
                    Id = aggregateId.ToString(),
                    Type = nameof(ExampleAggregate),
                    Version = 1
                }, cancellationToken);

                await UnitOfWork.Commit(cancellationToken);

                await PublishEvents(generatedEvents, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                // TODO: Add logging here
                throw;
            }

            return aggregateId;
        }

        private async Task PublishEvents(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            var exampleAddedEvent = domainEvents.OfType<ExampleEvent<ExampleAddedEventData>>().FirstOrDefault();

            await MessagePublisher.Publish("added-examples", exampleAddedEvent, cancellationToken);
        }

        public Task<Unit> Handle(ExampleAddressAddCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<long> Handle(SubExampleAddCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
