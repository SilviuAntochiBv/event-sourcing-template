using System;
using IdGen;

using EventBasedServiceTemplate.Domain;

namespace EventBasedServiceTemplate.Behavior
{
    public class UniqueIdGenerator : IUniqueIdGenerator
    {
        private readonly IdGenerator _longIdGenerator;

        public UniqueIdGenerator()
        {
            _longIdGenerator = new IdGenerator(0);
        }

        public long GenerateNumericId()
        {
            return _longIdGenerator.CreateId();
        }

        public Guid GenerateUuid()
        {
            return Guid.NewGuid();
        }
    }
}
