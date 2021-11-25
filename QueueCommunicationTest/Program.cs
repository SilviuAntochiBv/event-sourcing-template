using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using NATS.Client;
using NATS.Client.Rx;
using NATS.Client.Rx.Ops;

using QueueCommunicationTest.QueueEvents;

namespace QueueCommunicationTest
{
    internal class Program
    {
        private static readonly ManualResetEvent _quitEvent = new(false);

        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        static async Task Main(string[] args)
        {
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                _quitEvent.Set();
                eventArgs.Cancel = true;
            };

            using var natsConnection = new ConnectionFactory().CreateConnection("nats://localhost:4222");
            var addedExampleSubscription = natsConnection
                .Observe("added-examples")
                .Where(message => message.Data?.Any() == true)
                .Select(validMessage => DecodeMessageFromByteArray<ExampleEvent<ExampleAddedEventData>>(validMessage.Data))
                .Where(parsedMessage => parsedMessage != null)
                .Subscribe(new MessageObserver(natsConnection));

            _quitEvent.WaitOne();

            addedExampleSubscription.Dispose();
        }

        class MessageObserver : IObserver<ExampleEvent<ExampleAddedEventData>>
        {
            private readonly Random randomizer;
            private readonly IConnection _connection;

            public MessageObserver(IConnection connection)
            {
                _connection = connection;
                randomizer = new Random();
            }

            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
                Console.Error.WriteLine($"Error occurred while observing: {error.Message}");
            }

            public void OnNext(ExampleEvent<ExampleAddedEventData> value)
            {
                Console.WriteLine($"Example with id: {value.AggregateId} and representation: [{value.Data.Name}, {value.Data.Value}]");

                var processedEvent = new ExampleEvent<ExampleProcessedEventData>(value.AggregateId, new ExampleProcessedEventData(randomizer.Next(0, 10) % 2 == 0 ? "Success" : "Failure"));
                var processedEventAsJson = JsonSerializer.Serialize(processedEvent, JsonOptions);
                _connection.Publish("console-app-test", Encoding.UTF8.GetBytes(processedEventAsJson));
            }
        }

        private static TMessage DecodeMessageFromByteArray<TMessage>(byte[] binaryMessage)
        {
            var jsonMessage = Encoding.UTF8.GetString(binaryMessage);

            try
            {
                var result = JsonSerializer.Deserialize<TMessage>(jsonMessage, JsonOptions);
                return result;
            }
            catch (Exception exc)
            {
                return default;
            }
        }
    }
}
