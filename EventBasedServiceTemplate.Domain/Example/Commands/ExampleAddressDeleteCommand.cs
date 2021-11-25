using MediatR;

namespace EventBasedServiceTemplate.Domain.Example.Commands
{
    public class ExampleAddressDeleteCommand : IRequest
    {
        public string ExistingStreetName { get; set; }

        public string ExistingTown { get; set; }
    }
}
