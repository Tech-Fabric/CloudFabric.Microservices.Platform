using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using EventStore.ClientAPI;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CloudFabric.EventStore
{
    public class EventStore : IEventStore
    {
        private static long currentSequenceNumber = 0;
        private static readonly IList<Event> database = new List<Event>();
        private const string connectionString = "ConnectTo=discover://admin:changeit@127.0.0.1:2113/";
        private IEventStoreConnection connection = EventStoreConnection.Create(connectionString);

        public async Task Raise(string eventName, string eventType, string stream, object content)
        {
            await connection.ConnectAsync().ConfigureAwait(false);
            var contentJson = JsonConvert.SerializeObject(content);

            var metaDataJson = JsonConvert.SerializeObject(
                              new EventMetadata
                              {
                                  OccurredAt = DateTimeOffset.Now,
                                  EventName = eventName
                              });

            var eventData = new EventData(Guid.NewGuid(), eventType, isJson: true, data: Encoding.UTF8.GetBytes(contentJson), metadata: Encoding.UTF8.GetBytes(metaDataJson));

            await connection.AppendToStreamAsync(stream, ExpectedVersion.Any, eventData);
        }

        public async Task<IEnumerable<Event>> GetEvents(string stream, long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            await connection.ConnectAsync().ConfigureAwait(false);

            var result = await connection.ReadStreamEventsForwardAsync(stream, start: (int)firstEventSequenceNumber, count: (int)(lastEventSequenceNumber - firstEventSequenceNumber), resolveLinkTos: false).ConfigureAwait(false);

            return result.Events
                    .Select(ev =>
                        new
                        {
                            Content = JsonConvert.DeserializeObject(
                            Encoding.UTF8.GetString(ev.Event.Data)),
                            Metadata = JsonConvert.DeserializeObject<EventMetadata>(
                            Encoding.UTF8.GetString(ev.Event.Data))
                        })
                    .Select((ev, i) =>
                        new Event(
                        i + firstEventSequenceNumber,
                        ev.Metadata.OccurredAt,
                        ev.Metadata.EventName,
                        ev.Content));
        }
    }
}
