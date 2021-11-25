using EventBasedServiceTemplate.Domain.Contracts.Persistence;

using MediatR;

using QueueClient.Contracts;

namespace EventBasedServiceTemplate.Behavior.Example.Handlers
{
    public abstract class BaseCommandHandler
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected IMediator Mediator { get; }
        protected IMessagePublisher MessagePublisher { get; }

        protected BaseCommandHandler(
            IUnitOfWork unitOfWork,
            IMediator mediator, 
            IMessagePublisher messagePublisher)
        {
            UnitOfWork = unitOfWork;
            Mediator = mediator;
            MessagePublisher = messagePublisher;
        }
    }
}
