using MediatR;

namespace EventBasedServiceTemplate.Domain.Example.Commands
{
    public class ExampleUpdateCommand : IRequest
    {
        public string NewName { get; set; }

        public int? NewValue { get; set; }
    }
}
