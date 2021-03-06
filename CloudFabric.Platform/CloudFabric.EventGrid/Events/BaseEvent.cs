﻿using System;
using System.Collections.Generic;
using System.Text;
using CloudFabric.Library.Common.Utilities;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;

namespace CloudFabric.EventGrid.Events
{
    public class BaseEvent : BaseEvent<object>
    {

    }
    public class BaseEvent<TDataType> : EventGridEvent
    {
        [JsonIgnore]
        public BaseEventData<TDataType> TypedData
        {
            get
            {
                return MapperUtility.Map<object, BaseEventData<TDataType>>(Data);
            }
            set
            {
                Data = value;
            }
        }

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
