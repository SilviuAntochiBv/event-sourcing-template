using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventBasedServiceTemplate.Domain;
using EventBasedServiceTemplate.Domain.Contracts.Persistence;
using EventBasedServiceTemplate.Domain.Example.Events;
using EventBasedServiceTemplate.Domain.Example.Events.Incoming;

using Microsoft.Extensions.DependencyInjection;

using QueueClient.Contracts;

namespace EventBasedServiceTemplate.Behavior.Example.Consumers
{
    public class ExampleProcessedEventConsumer : IMessageConsumer<ExampleEvent<ExampleProcessedEventData>>
    {
        private IUnitOfWork _unitOfWork;

        public ExampleProcessedEventConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _unitOfWork = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IUnitOfWork>();
        }

        public async Task Consume(ExampleEvent<ExampleProcessedEventData> message, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.EventStoreRepository.UpdateEventStore(new List<DomainEvent> { message }, cancellationToken);
            await _unitOfWork.Commit();
        }
    }
}
