using System;

namespace QueueClient.Contracts
{
    public interface ISubscriptionManager
    {
        IDisposable Subscribe<TMessage>(string queueName);
    }
}
