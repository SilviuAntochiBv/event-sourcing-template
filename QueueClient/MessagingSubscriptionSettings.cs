using Microsoft.Extensions.DependencyInjection;

using QueueClient.Contracts;

namespace QueueClient
{
    public class MessagingSubscriptionSettings
    {
        private readonly IServiceCollection _serviceCollection;

        internal MessagingSubscriptionSettings(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void RegisterForListening<TMessage, TConsumerImpl>()
            where TConsumerImpl : class, IMessageConsumer<TMessage>
        {
            _serviceCollection.AddSingleton<IMessageConsumer<TMessage>, TConsumerImpl>();
        }
    }
}
