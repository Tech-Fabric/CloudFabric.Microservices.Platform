using Microsoft.AspNetCore.Mvc;

namespace CloudFabric.EventStore
{
    [Route("api/eventsfeed")]
    public class EventsFeedController : Controller
    {
        IEventStore eventStore;
        public EventsFeedController(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        [HttpGet("/{stream}")]
        public IActionResult GetEvents(dynamic parameters)
        {
            long firstEventSequenceNumber, lastEventSequenceNumber;

            if (!long.TryParse(this.Request.Query["start"], out firstEventSequenceNumber))
            {
                firstEventSequenceNumber = 0;
            }

            if (!long.TryParse(this.Request.Query["end"], out lastEventSequenceNumber))
            {
                lastEventSequenceNumber = long.MaxValue;
            }

            return eventStore.GetEvents(parameters.stream, firstEventSequenceNumber, lastEventSequenceNumber);
        }
    }
}
