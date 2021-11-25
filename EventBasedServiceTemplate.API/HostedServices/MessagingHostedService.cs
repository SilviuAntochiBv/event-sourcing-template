using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventBasedServiceTemplate.Domain.Example.Events;
using EventBasedServiceTemplate.Domain.Example.Events.Incoming;

using Microsoft.Extensions.Hosting;

using QueueClient.Contracts;

namespace EventBasedServiceTemplate.API.HostedServices
{
    public class MessagingHostedService : BackgroundService
    {
        private readonly ISubscriptionManager _subscriptionManager;
        private readonly List<IDisposable> _activeSubscriptions = new();

        public MessagingHostedService(ISubscriptionManager subscriptionManager)
        {
            _subscriptionManager = subscriptionManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CreateSubscription<ExampleEvent<ExampleProcessedEventData>>("console-app-test");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(-1, stoppingToken);
            }
        }

        private void CreateSubscription<TMessage>(string queueName)
        {
            var subscription = _subscriptionManager.Subscribe<TMessage>(queueName);
            _activeSubscriptions.Add(subscription);
        }

        #region IDisposable implementation
        public override void Dispose()
        {
            base.Dispose();

            foreach (var subscription in _activeSubscriptions)
            {
                subscription.Dispose();
            }
            _activeSubscriptions.Clear();
        }
        #endregion
    }
}
