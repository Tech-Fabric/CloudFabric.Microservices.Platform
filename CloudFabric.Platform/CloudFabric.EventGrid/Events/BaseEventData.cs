using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.EventGrid.Events
{
    public class BaseEventData<TDataType>
    {
        public TDataType PreviousValue { get; set; }
        public TDataType NewValue { get; set; }
    }
    public class BaseEventData : BaseEventData<object>
    {

    }
}
