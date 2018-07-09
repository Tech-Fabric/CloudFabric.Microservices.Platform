using System.Threading.Tasks;
using System.Collections.Generic;

namespace CloudFabric.EventStore
{
    public interface IEventStore
    {
        Task Raise(string eventName, string eventType, string stream, object content);
        Task<IEnumerable<Event>> GetEvents(string stream, long firstEventSequenceNumber, long lastEventSequenceNumber);
    }
}
