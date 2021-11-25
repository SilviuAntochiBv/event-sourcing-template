using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EventBasedServiceTemplate.Domain.Contracts.Persistence;
using EventBasedServiceTemplate.Domain.Example.AggregateRoots;
using EventBasedServiceTemplate.Domain.Example.Queries;
using EventBasedServiceTemplate.Domain.Example.Queries.Results;

using MediatR;

namespace EventBasedServiceTemplate.Behavior.Example.Handlers
{
    public class QueryHandlers : IRequestHandler<GetExampleByIdQuery, ExampleOutputDto>
    {
        private readonly IAggregateQueryRepository<ExampleAggregate, Guid> _queryRepository;

        public QueryHandlers(IAggregateQueryRepository<ExampleAggregate, Guid> queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<ExampleOutputDto> Handle(GetExampleByIdQuery request, CancellationToken cancellationToken)
        {
            var aggregate = await _queryRepository.GetById(nameof(ExampleAggregate), request.Id, cancellationToken);

            return new ExampleOutputDto
            {
                Id = request.Id,
                Name = aggregate.ParentName,
                Value = aggregate.ParentValue,
                ProcessingStatus = aggregate.ProcessingStatus,
                Addresses = aggregate?.Addresses.Select(address => new ExampleAddressOutputDto
                {
                    StreetName = address.StreetName,
                    Town = address.Town
                }) ?? Enumerable.Empty<ExampleAddressOutputDto>(),
                SubExamples = aggregate?.SubExamples.Select(subExample => new SubExampleOutputDto
                {
                    Id = subExample.Id,
                    Description = subExample.ChildDescription
                }) ?? Enumerable.Empty<SubExampleOutputDto>(),
            };
        }
    }
}
