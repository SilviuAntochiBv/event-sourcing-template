using System.Text.Json;

namespace QueueClient.Settings
{
    internal static class MessageContentOptions
    {
        internal static JsonSerializerOptions MessageSerializationOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
