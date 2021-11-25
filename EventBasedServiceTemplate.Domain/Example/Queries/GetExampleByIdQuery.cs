using System;

using EventBasedServiceTemplate.Domain.Example.Queries.Results;

using MediatR;

namespace EventBasedServiceTemplate.Domain.Example.Queries
{
    public record GetExampleByIdQuery(Guid Id) : IRequest<ExampleOutputDto>;
}
