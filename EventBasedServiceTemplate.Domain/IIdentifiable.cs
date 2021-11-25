using System;

namespace EventBasedServiceTemplate.Domain
{
    public interface IIdentifiable<T> where T : struct, IEquatable<T>
    {
        T Id { get; }
    }
}
