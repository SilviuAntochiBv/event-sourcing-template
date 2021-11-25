using MediatR;

namespace EventBasedServiceTemplate.Domain.Example.Commands
{
    public class SubExampleUpdateCommand : IRequest
    {
        public long Id { get; set; }

        public string NewDescription { get; set; }
    }
}
