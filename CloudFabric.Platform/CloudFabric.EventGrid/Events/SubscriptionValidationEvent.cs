using CloudFabric.EventGrid.Enums;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.EventGrid.Events
{
    public class SubscriptionValidationEvent : BaseEvent
    {
        public new string EventType {
            get
            {
                return EventTypeEnumFactory.SubscriptionValidationEvent;
            }
            set
            {

            }
        }
    }
}
