using System;
using System.Linq;
using System.Text.Json;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using NATS.Client;
using NATS.Client.Rx;
using NATS.Client.Rx.Ops;

using QueueClient.Contracts;
using static QueueClient.Settings.MessageContentOptions;

namespace QueueClient
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly IServiceProvider _serviceProvider;

        public SubscriptionManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDisposable Subscribe<TMessage>(string queueName)
        {
            var connection = _serviceProvider.GetRequiredService<IConnection>();
            var consumer = _serviceProvider.GetRequiredService<IMessageConsumer<TMessage>>();

            return connection
                .Observe(queueName)
                .Where(message => message.Data?.Any() == true)
                .Select(validMessage => AsMessage<TMessage>(validMessage.Data))
                .Subscribe(message => consumer.Consume(message));
        }

        private static TMessage AsMessage<TMessage>(byte[] binaryMessage)
        {
            return JsonSerializer.Deserialize<TMessage>(Encoding.UTF8.GetString(binaryMessage), MessageSerializationOptions);
        }
    }
}
