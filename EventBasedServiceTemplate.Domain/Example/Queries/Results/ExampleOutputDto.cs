using System;
using System.Collections.Generic;

namespace EventBasedServiceTemplate.Domain.Example.Queries.Results
{
    public class ExampleOutputDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public string ProcessingStatus { get; set; }

        public IEnumerable<ExampleAddressOutputDto> Addresses { get; set; }

        public IEnumerable<SubExampleOutputDto> SubExamples { get; set; }
    }
}
