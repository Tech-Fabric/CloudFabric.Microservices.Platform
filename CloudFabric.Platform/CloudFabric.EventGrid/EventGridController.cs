using CloudFabric.EventGrid.Enums;
using CloudFabric.EventGrid.Events;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudFabric.EventGrid
{
    public abstract class EventGridController
    {
        protected async Task<SubscriptionValidationResponse> Run(BaseEvent[] eventGridEvents)
        {


            foreach (BaseEvent eventGridEvent in eventGridEvents)
            {
                //JObject dataObject = eventGridEvent.Data as JObject;

                // Deserialize the event data into the appropriate type based on event type
                if (eventGridEvent.EqualsType(EventTypeEnumFactory.SubscriptionValidationEvent))
                {

                    var responseData = new SubscriptionValidationResponse
                    {
                        ValidationResponse = ((SubscriptionValidationEventData)eventGridEvent.Data)?.ValidationUrl
                    };
                    return responseData;
                }

                await HandleEventAsync(eventGridEvent);
            }

            await Task.CompletedTask;
            return null;
        }

        protected abstract Task HandleEventAsync(BaseEvent e);
    }
}
