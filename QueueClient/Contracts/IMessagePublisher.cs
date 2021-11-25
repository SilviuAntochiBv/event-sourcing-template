using System.Threading;
using System.Threading.Tasks;

namespace QueueClient.Contracts
{
    public interface IMessagePublisher
    {
        Task Publish<TMessage>(string queueName, TMessage message, CancellationToken cancellationToken = default);
    }
}
