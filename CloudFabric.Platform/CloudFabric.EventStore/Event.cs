using System;

namespace CloudFabric.EventStore
{
    public struct Event
    {
        public string Name { get; }
        public object Content { get; }
        public long SequenceNumber { get; }
        public DateTimeOffset OccuredAt { get; }

        public Event(long sequenceNumber, DateTimeOffset occuredAt, string name, object content)
        {
            this.Name = name;
            this.Content = content;
            this.OccuredAt = occuredAt;
            this.SequenceNumber = sequenceNumber;
        }
    }
}
