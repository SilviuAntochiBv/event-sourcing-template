
using Microsoft.Extensions.DependencyInjection;

using NATS.Client;

using QueueClient.Contracts;

namespace QueueClient.Settings
{
    public class MessagingConnectivitySettings
    {
        private string hostName = "localhost";
        private string port = "4222";

        internal MessagingConnectivitySettings(IServiceCollection serviceCollection)
        {
            var connectionOptions = ConnectionFactory.GetDefaultOptions();
            connectionOptions.Url = Url;
            connectionOptions.AllowReconnect = true;

            var messagingConnection = new ConnectionFactory().CreateConnection(connectionOptions);

            serviceCollection.AddSingleton(messagingConnection);
            serviceCollection.AddSingleton<IMessagePublisher, MessagePublisher>();
            serviceCollection.AddSingleton<ISubscriptionManager, SubscriptionManager>();
        }

        public string HostName
        {
            get => hostName;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    hostName = value;
            }
        }

        public string Port
        {
            get => port;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    port = value;
            }
        }

        private string Url => $"nats://{HostName}:{Port}";
    }
}
