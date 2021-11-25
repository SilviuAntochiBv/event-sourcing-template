using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QueueCommunicationTest
{
    internal class ApplicationConfig
    {
        [JsonPropertyName("channels")]
        public IEnumerable<string> Channels { get; set; }
    }
}
