using MediatR;

namespace EventBasedServiceTemplate.Domain.Example.Commands
{
    public class SubExampleDeleteCommand : IRequest
    {
        public long Id { get; set; }
    }
}
