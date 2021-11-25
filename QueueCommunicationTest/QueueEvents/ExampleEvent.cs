using System.Text.Json.Serialization;

namespace QueueCommunicationTest.QueueEvents
{
    internal record ExampleEvent<TData>(string AggregateId, TData Data);
}
