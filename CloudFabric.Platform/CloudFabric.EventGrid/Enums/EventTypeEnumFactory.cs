using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.EventGrid.Enums
{
    public class EventTypeEnumFactory
    {
        public const string SubscriptionValidationEvent = "Microsoft.EventGrid.SubscriptionValidationEvent";

        public const string UserCreated = "UserCreated";
        public const string UserUpdated = "UserUpdated";
        public const string UserLockedout = "UserLockedOut";
        public const string UserDeleted = "UserDeleted";

        public const string LeadReceived = "LeadReceived";
        public const string LeadProcessed = "LeadProcessed";
        public const string LeadCreated = "LeadCreated";
        public const string LeadUpdated = "LeadUpdated";
        public const string LeadMarkedAsDuplicate = "LeadMarkedAsDuplicate";
        public const string LeadStatusChanged = "LeadStatusChanged";
        public const string LeadAssigned = "LeadAssigned";

        public const string LeadAssignmentRuleGroupCreated = "LeadAssignmentRuleGroupCreated";
        public const string LeadAssignmentRuleGroupUpdated = "LeadAssignmentRuleGroupUpdated";
        public const string LeadAssignmentRuleGroupDeleted = "LeadAssignmentRuleGroupDeleted";



        public static string Get(EventTypeEnum eventType)
        {
            switch (eventType)
            {
                case EventTypeEnum.SubscriptionValidationEvent: return SubscriptionValidationEvent;

                case EventTypeEnum.UserCreated: return UserCreated;
                case EventTypeEnum.UserDeleted: return UserDeleted;
                case EventTypeEnum.UserLockedOut: return UserLockedout;
                case EventTypeEnum.UserUpdated: return UserUpdated;

                case EventTypeEnum.LeadReceived: return LeadReceived;
                case EventTypeEnum.LeadProcessed: return LeadProcessed;
                case EventTypeEnum.LeadCreated: return LeadCreated;
                case EventTypeEnum.LeadUpdated: return LeadUpdated;
                case EventTypeEnum.LeadMarkedAsDuplicate: return LeadMarkedAsDuplicate;
                case EventTypeEnum.LeadStatusChanged: return LeadStatusChanged;
                case EventTypeEnum.LeadAssigned: return LeadAssigned;


                case EventTypeEnum.LeadAssignmentRuleGroupCreated: return LeadAssignmentRuleGroupCreated;
                case EventTypeEnum.LeadAssignmentRuleGroupUpdated: return LeadAssignmentRuleGroupUpdated;
                case EventTypeEnum.LeadAssignmentRuleGroupDeleted: return LeadAssignmentRuleGroupDeleted;


                default: throw new Exception("Invalid enum value");
            }
        }

        public static EventTypeEnum Get(string eventType)
        {
            switch (eventType)
            {
                case SubscriptionValidationEvent: return EventTypeEnum.SubscriptionValidationEvent;

                case UserCreated: return EventTypeEnum.UserCreated;
                case UserDeleted: return EventTypeEnum.UserDeleted;
                case UserLockedout: return EventTypeEnum.UserLockedOut;
                case UserUpdated: return EventTypeEnum.UserUpdated;

                case LeadReceived: return EventTypeEnum.LeadReceived;
                case LeadProcessed: return EventTypeEnum.LeadProcessed;
                case LeadCreated: return EventTypeEnum.LeadCreated;
                case LeadUpdated: return EventTypeEnum.LeadUpdated;
                case LeadMarkedAsDuplicate: return EventTypeEnum.LeadMarkedAsDuplicate;
                case LeadStatusChanged: return EventTypeEnum.LeadStatusChanged;
                case LeadAssigned: return EventTypeEnum.LeadAssigned;

                case LeadAssignmentRuleGroupCreated: return EventTypeEnum.LeadAssignmentRuleGroupCreated;
                case LeadAssignmentRuleGroupUpdated: return EventTypeEnum.LeadAssignmentRuleGroupUpdated;
                case LeadAssignmentRuleGroupDeleted: return EventTypeEnum.LeadAssignmentRuleGroupDeleted;

                default: throw new Exception("Invalid enum value");
            }
        }
    }
}
