using System;
using System.Collections.Generic;

using MediatR;

namespace EventBasedServiceTemplate.Domain.Example.Commands
{
    public class ExampleAddCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public IEnumerable<SubExampleAddCommand> SubExamples { get; set; }

        public IEnumerable<ExampleAddressAddCommand> Addresses { get; set; }
    }
}
