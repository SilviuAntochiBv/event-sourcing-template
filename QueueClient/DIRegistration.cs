using System;

using QueueClient;
using QueueClient.Settings;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIRegistration
    {
        /// <summary>
        /// Sets up the messaging system
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="messagingSettings">Entrypoint for custom settings like hostname, port and consumers. The default value for hostname and port are localhost and 4222 respectively. If no consumers are set, the application will only be able to publish messages</param>
        /// <returns></returns>
        public static void SetupMessaging(this IServiceCollection serviceCollection, Action<MessagingConnectivitySettings> messagingSettings = null, Action<MessagingSubscriptionSettings> subscriptionSettings = null)
        {
            var messagingConnectivitySettings = new MessagingConnectivitySettings(serviceCollection);
            messagingSettings?.Invoke(messagingConnectivitySettings);

            var messagingSubscriptionSettings = new MessagingSubscriptionSettings(serviceCollection);
            subscriptionSettings?.Invoke(messagingSubscriptionSettings);
        }
    }
}
