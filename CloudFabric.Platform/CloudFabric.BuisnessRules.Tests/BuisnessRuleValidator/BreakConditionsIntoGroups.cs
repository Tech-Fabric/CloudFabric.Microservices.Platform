using CloudFabric.BuisnessRules.Enums;
using CloudFabric.BuisnessRules.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CloudFabric.BuisnessRules.Tests
{
    public class BreakConditionsIntoGroups
    {
        [Fact] void RulesBrokenIntoRuleGroups1()
        {
            var rules = new List<Condition>
            {
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.Or)
            };
            Assert.True(BuisnessRulesValidator.GetConditionOrGroups(rules).Count == 1);
        }
        [Fact]
        void RulesBrokenIntoRuleGroups2()
        {
            var rules = new List<Condition>
            {
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.And)
            };
            Assert.True(BuisnessRulesValidator.GetConditionOrGroups(rules).Count == 1);
        }
        [Fact]
        void RulesBrokenIntoRuleGroups3()
        {
            var rules = new List<Condition>
            {
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, null)
            };
            Assert.True(BuisnessRulesValidator.GetConditionOrGroups(rules).Count == 1);
        }
        [Fact]
        void RulesBrokenIntoRuleGroups4()
        {
            var rules = new List<Condition>
            {
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.Or),
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.Or),
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, null)
            };
            Assert.True(BuisnessRulesValidator.GetConditionOrGroups(rules).Count == 1);
        }
        [Fact]
        void RulesBrokenIntoRuleGroups5()
        {
            var rules = new List<Condition>
            {
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.And),
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.Or),
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, null)
            };
            Assert.True(BuisnessRulesValidator.GetConditionOrGroups(rules).Count == 2);
        }
        [Fact]
        void RulesBrokenIntoRuleGroups6()
        {
            var rules = new List<Condition>
            {
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.And),
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.Or),
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.And),
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.Or),
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.Or),
                new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, null),
            };
            Assert.True(BuisnessRulesValidator.GetConditionOrGroups(rules).Count == 3);
        }
    }
}
