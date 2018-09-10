using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.EventGrid.Enums
{
    public enum EventTypeEnum
    {


        UserCreated = 0,
        UserUpdated = 1,
        UserLockedOut = 2,
        UserDeleted = 3,

        LeadReceived = 100,
        LeadProcessed = 101,
        LeadCreated = 102,
        LeadUpdated = 103,
        LeadMarkedAsDuplicate = 104,
        LeadStatusChanged = 105,
        LeadAssigned = 106,

        LeadAssignmentRuleGroupCreated = 200,
        LeadAssignmentRuleGroupUpdated = 201,
        LeadAssignmentRuleGroupDeleted = 202,





        SubscriptionValidationEvent = 10000,

    }
}
