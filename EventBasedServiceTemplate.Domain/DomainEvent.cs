using System;

namespace EventBasedServiceTemplate.Domain
{
    public class DomainEvent
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public string AggregateName { get; set; }

        public string AggregateId { get; set; }

        public DateTime OccurredAt { get; set; } = DateTime.Now;

        public virtual string RawEventData { get; set; }
    }
}
