using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.EventGrid.Models;


namespace CloudFabric.EventGrid.Events
{
    public abstract class BaseEvent : EventGridEvent
    {
        public abstract string Type {get;}

        public new string EventType
        {
            get => Type;
            set { }
        }


        public bool EqualsType(BaseEvent e)
        {
            return EqualsType(e.EventType);
        }
        public bool EqualsType(string type)
        {
            return string.Equals(EventType.ToLower(), type);
        }
    }
}
