using System;

namespace CloudFabric.EventStore
{
    public class EventMetadata
    {
        public DateTimeOffset OccurredAt { get; set; }
        public string EventName { get; set; }
    }
}
