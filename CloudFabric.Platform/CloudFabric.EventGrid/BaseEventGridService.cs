using CloudFabric.EventGrid.Enums;
using CloudFabric.EventGrid.Events;
using CloudFabric.Library.Common.Utilities;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudFabric.EventGrid
{
    public abstract class BaseEventGridService : IBaseEventGridService
    {
        public async Task<SubscriptionValidationResponse> Run(EventGridEvent[] eventGridEvents)
        {


            foreach (EventGridEvent eventGridEvent in eventGridEvents)
            {
                //JObject dataObject = eventGridEvent.Data as JObject;

                var test = eventGridEvent.EventType.ToLower() == EventTypeEnumFactory.SubscriptionValidationEvent.ToLower();
                // Deserialize the event data into the appropriate type based on event type
                if (eventGridEvent.EventType.ToLower() == EventTypeEnumFactory.SubscriptionValidationEvent.ToLower())
                {

                    var json = JsonConvert.SerializeObject(eventGridEvent.Data);
                    var obj = JsonConvert.DeserializeObject<SubscriptionValidationEventData>(json);

                    var responseData = new SubscriptionValidationResponse
                    {
                        
                        ValidationResponse = obj?.ValidationCode
                    };
                    return responseData;
                }

                await HandleEventAsync(MapperUtility.Map<EventGridEvent, BaseEvent>(eventGridEvent));
            }

            await Task.CompletedTask;
            return null;
        }

        protected abstract Task HandleEventAsync(BaseEvent e);
    }
}
