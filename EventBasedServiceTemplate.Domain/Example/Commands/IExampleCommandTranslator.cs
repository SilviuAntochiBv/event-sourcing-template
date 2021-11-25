using System;

namespace EventBasedServiceTemplate.Domain.Example.Commands
{
    public interface IExampleCommandTranslator :
        ICommandTranslator<ExampleAddCommand, Guid>,
        ICommandTranslator<ExampleUpdateCommand, Guid>,
        ICommandTranslator<ExampleAddressAddCommand, Guid>,
        ICommandTranslator<ExampleAddressDeleteCommand, Guid>,
        ICommandTranslator<SubExampleAddCommand, Guid>,
        ICommandTranslator<SubExampleUpdateCommand, Guid>,
        ICommandTranslator<SubExampleDeleteCommand, Guid>
    {
    }
}
