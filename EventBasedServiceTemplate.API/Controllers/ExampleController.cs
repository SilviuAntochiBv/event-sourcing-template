using System;
using System.Threading.Tasks;

using EventBasedServiceTemplate.API.Configuration;
using EventBasedServiceTemplate.Domain.Contracts.Persistence;
using EventBasedServiceTemplate.Domain.Example.Commands;
using EventBasedServiceTemplate.Domain.Example.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace EventBasedServiceTemplate.API.Controllers
{
    [Route("examples")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRuntimeConfigurationStore _runtimeConfigurationStore;

        public ExampleController(
            IMediator mediator,
            IRuntimeConfigurationStore runtimeConfigurationStore)
        {
            _mediator = mediator;
            _runtimeConfigurationStore = runtimeConfigurationStore;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExample(ExampleAddCommand exampleAddCommand)
        {
            var newExampleId = await _mediator.Send(exampleAddCommand);

            return CreatedAtRoute("RetrieveExample", new { exampleId = newExampleId }, null);
        }

        [HttpGet("{exampleId}", Name = "RetrieveExample")]
        public async Task<IActionResult> GetExample(Guid exampleId)
        {
            var retrievedEntity = await _mediator.Send(new GetExampleByIdQuery(exampleId));

            return Ok(retrievedEntity);
        }

        [HttpGet("configuration-example")]
        public async Task<IActionResult> GetRuntimeConfigurationTest()
        {
            var exampleConfiguration = await _runtimeConfigurationStore.GetConfiguration<ExampleConfiguration>("example-configuration");

            return Ok(new
            {
                Text = exampleConfiguration.TextProperty,
                Value = exampleConfiguration.ValueProperty
            });
        }
    }
}
