using System;

namespace EventBasedServiceTemplate.Domain
{
    public interface IUniqueIdGenerator
    {
        Guid GenerateUuid();

        long GenerateNumericId();
    }
}
