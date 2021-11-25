using EventBasedServiceTemplate.Domain.Contracts.Persistence;
using EventBasedServiceTemplate.Domain.Example.Commands;

using MediatR;

using QueueClient.Contracts;

using System.Threading;
using System.Threading.Tasks;

namespace EventBasedServiceTemplate.Behavior.Example.Handlers
{
    public class DeleteCommandHandlers : BaseCommandHandler,
        IRequestHandler<ExampleAddressDeleteCommand>,
        IRequestHandler<SubExampleDeleteCommand>
    {
        public DeleteCommandHandlers(IUnitOfWork unitOfWork, IMediator mediator, IMessagePublisher messagePublisher) : base(unitOfWork, mediator, messagePublisher)
        {
        }

        public Task<Unit> Handle(ExampleAddressDeleteCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Unit> Handle(SubExampleDeleteCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
