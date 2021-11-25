using System.Text.Json;

namespace EventBasedServiceTemplate.Domain
{
    public class DomainEventWithData<TEventData> : DomainEvent
        where TEventData : class
    {
        private string serializedData;

        public TEventData Data { get; set; }

        public string SerializedData
        {
            get
            {
                if (string.IsNullOrEmpty(serializedData))
                {
                    serializedData = JsonSerializer.Serialize(Data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
                return serializedData;
            }
        }

        public override string RawEventData
        {
            get => base.RawEventData ?? SerializedData;
            set => base.RawEventData = value;
        }

        public DomainEventWithData()
        {
            Type = typeof(TEventData).Name;
        }
    }
}
