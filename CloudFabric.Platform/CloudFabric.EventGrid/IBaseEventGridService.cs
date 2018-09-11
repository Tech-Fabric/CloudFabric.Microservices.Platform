using CloudFabric.EventGrid.Events;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudFabric.EventGrid
{
    public interface IBaseEventGridService
    {
        Task<SubscriptionValidationResponse> Run(BaseEvent[] eventGridEvents);
    }
}
