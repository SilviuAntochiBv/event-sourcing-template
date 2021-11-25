using System;
using System.Threading;
using System.Threading.Tasks;

using EventBasedServiceTemplate.Domain.Contracts.Persistence;
using EventBasedServiceTemplate.Domain.Example.Commands;

using MediatR;

using QueueClient.Contracts;

namespace EventBasedServiceTemplate.Behavior.Example.Handlers
{
    public class UpdateCommandHandlers : BaseCommandHandler,
        IRequestHandler<ExampleUpdateCommand>,
        IRequestHandler<SubExampleUpdateCommand>
    {
        public UpdateCommandHandlers(IUnitOfWork unitOfWork, IMediator mediator, IMessagePublisher messagePublisher) : base(unitOfWork, mediator, messagePublisher)
        {
        }

        public Task<Unit> Handle(ExampleUpdateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Unit> Handle(SubExampleUpdateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
