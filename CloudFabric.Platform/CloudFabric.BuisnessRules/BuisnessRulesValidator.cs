using CloudFabric.BuisnessRules.Enums;
using CloudFabric.BuisnessRules.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.BuisnessRules
{
    public class BuisnessRulesValidator
    {
        public static bool IsValid(Condition condition)
        {
            return IsValid(new List<Condition> { condition });
        }

        public static bool IsValid(List<Condition> conditions)
        {
            var groupedConditions = GetConditionOrGroups(conditions);

            return groupedConditions.TrueForAll(conditionGroup => conditionGroup.Find(condition => condition.IsTrue()) != null);
        }

        /// <summary>
        /// Breaks the list of conditions into groups of or groups; where it only requires 1 condition in the set to be true in order to return true.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public static List<List<Condition>> GetConditionOrGroups(List<Condition> conditions)
        {
            List<List<Condition>> groups = new List<List<Condition>>();

            conditions.ForEach(condition =>
            {
                if(groups.Count == 0)
                {
                    groups.Add(new List<Condition> { });
                }

                groups[groups.Count - 1].Add(condition);

                if(condition.Connector == ConnectorTypeEnum.And)
                {
                    groups.Add(new List<Condition> { });
                }
            });

            groups.RemoveAll(c => c.Count == 0);

            return groups;
        }
    }
}
