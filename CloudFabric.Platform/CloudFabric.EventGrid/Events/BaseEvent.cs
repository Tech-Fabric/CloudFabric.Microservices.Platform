using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.EventGrid.Models;


namespace CloudFabric.EventGrid.Events
{
    public class BaseEvent : BaseEvent<object>
    {

    }
    public class BaseEvent<TDataType> : EventGridEvent
    {
        public new BaseEventData<TDataType> Data { get; set; }

        public bool EqualsType(BaseEvent e)
        {
            return EqualsType(e.EventType);
        }
        public bool EqualsType(string type)
        {
            return string.Equals(EventType.ToLower(), type.ToLower());
        }
    }
}
