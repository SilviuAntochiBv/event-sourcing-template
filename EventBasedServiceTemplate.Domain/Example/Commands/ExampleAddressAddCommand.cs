using MediatR;

namespace EventBasedServiceTemplate.Domain.Example.Commands
{
    public record ExampleAddressAddCommand(string NewStreetName, string NewTown) : IRequest;
}
