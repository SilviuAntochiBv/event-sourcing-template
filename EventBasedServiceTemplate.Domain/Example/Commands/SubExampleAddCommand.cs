using MediatR;

namespace EventBasedServiceTemplate.Domain.Example.Commands
{
    public class SubExampleAddCommand : IRequest<long>
    {
        public long Id { get; set; }

        public string NewDescription { get; set; }
    }
}
