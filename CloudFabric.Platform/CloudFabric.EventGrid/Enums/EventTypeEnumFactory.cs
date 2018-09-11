using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.EventGrid.Enums
{
    public class EventTypeEnumFactory
    {
        public const string SubscriptionValidationEvent = "Microsoft.EventGrid.SubscriptionValidationEvent";

        public static string Get(EventTypeEnum eventType)
        {
            switch (eventType)
            {
                case EventTypeEnum.SubscriptionValidationEvent: return SubscriptionValidationEvent;
            }
            return null;
        }

        public static EventTypeEnum? Get(string eventType)
        {
            switch (eventType)
            {
                case SubscriptionValidationEvent: return EventTypeEnum.SubscriptionValidationEvent;
            }
            return null;
        }
    }
}
