using System.Threading;
using System.Threading.Tasks;

namespace QueueClient.Contracts
{
    public interface IMessageConsumer<TMessage>
    {
        Task Consume(TMessage message, CancellationToken cancellationToken = default);
    }
}
