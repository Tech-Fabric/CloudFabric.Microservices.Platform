using CloudFabric.BuisnessRules.Enums;
using CloudFabric.BuisnessRules.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CloudFabric.BuisnessRules.Tests
{
    public class ValidateRule
    {
        [Fact]
        public void Test1()
        {
            //rule should be true (1 is 1)
            var rule = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, null);
            Assert.True(rule.IsTrue());

            //since rule is true, buisness validator should say that all rules are true
            Assert.True(BuisnessRulesValidator.IsValid(rule));
        }

        [Fact]
        public void Test2()
        {
            //rule should be false (1 is 2)
            var rule = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 2, null);
            Assert.False(rule.IsTrue());

            //since rule is false, buisness validator should say that not all rules pass
            Assert.False(BuisnessRulesValidator.IsValid(rule));
        }

        [Fact]
        public void Test3()
        {
            //rule should be false (1 isNot 1)
            var rule = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.IsNot), 1, null);
            Assert.False(rule.IsTrue());

            //since rule is false, buisness validator should say that not all rules pass
            Assert.False(BuisnessRulesValidator.IsValid(rule));
        }
        [Fact]
        public void Test4()
        {
            //rule should be true (1 isNot 2)
            var rule = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.IsNot), 2, null);
            Assert.True(rule.IsTrue());

            //since rule is true, buisness validator should say that all rules are true
            Assert.True(BuisnessRulesValidator.IsValid(rule));
        }


        [Fact]
        public void Test5()
        {
            // only one rule is true, but rule group should still equate to true
            var rule1 = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 2, ConnectorTypeEnum.Or); // false
            var rule2 = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.Or); // true 
            var rule3 = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 2, ConnectorTypeEnum.Or); // false
            Assert.False(rule1.IsTrue());
            Assert.True(rule2.IsTrue());
            Assert.False(rule3.IsTrue());

            var rules = new List<Condition> { rule1, rule2, rule3 };

            Assert.True(BuisnessRulesValidator.IsValid(rules));
        }

        [Fact]
        public void Test6()
        {
            // only one rule is true, but rule group should still equate to true
            var rule1 = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 2, ConnectorTypeEnum.Or); // false
            var rule2 = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.And); // true 
            var rule3 = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 2, null); // false
            Assert.False(rule1.IsTrue());
            Assert.True(rule2.IsTrue());
            Assert.False(rule3.IsTrue());

            var rules = new List<Condition> { rule1, rule2, rule3 };

            Assert.False(BuisnessRulesValidator.IsValid(rules));
        }
        [Fact]
        public void Test7()
        {
            // only one rule is true, but rule group should still equate to true
            var rule1 = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 2, ConnectorTypeEnum.Or); // false
            var rule2 = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, ConnectorTypeEnum.And); // true 
            var rule3 = new Condition(1, new Comparer(ComparerTypeEnum.Is, (int)IsTypeEnum.Is), 1, null); // true
            Assert.False(rule1.IsTrue());
            Assert.True(rule2.IsTrue());
            Assert.True(rule3.IsTrue());

            var rules = new List<Condition> { rule1, rule2, rule3 };

            Assert.True(BuisnessRulesValidator.IsValid(rules));
        }

    }
}
