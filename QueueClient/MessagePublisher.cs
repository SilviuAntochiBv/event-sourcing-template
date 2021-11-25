using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

using NATS.Client;

using QueueClient.Contracts;
using static QueueClient.Settings.MessageContentOptions;

namespace QueueClient
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IConnection _connection;

        public MessagePublisher(IConnection connection)
        {
            _connection = connection;
        }

        public async Task Publish<TMessage>(string queueName, TMessage message, CancellationToken cancellationToken = default)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message), "The published message cannot be null");

            var serializedMessage = JsonSerializer.Serialize(message, MessageSerializationOptions);
            var publishContent = Encoding.UTF8.GetBytes(serializedMessage);

            await Task.Factory.StartNew(() => _connection.Publish(queueName, publishContent), cancellationToken);
        }
    }
}
