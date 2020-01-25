using System;
using System.Collections.Generic;
using System.Linq;
using EssentialRules.Test.Rules;
using Xunit;

namespace EssentialRules.Test
{
    public class TimedBaseRuleTests
    {
        [Fact]
        public void CanFindFactsBeforeTimestamp()
        {
            var fakeTimedRule = CreateRule();
        }

        [Fact]
        public void CanFindFactBeforeOtherFact()
        {
            var rule = CreateRule();
            var keyValuePair = rule.FindAllTimed<string>(s => s.Contains("f")).First();
            IEnumerable<int> intsBefore = rule.FindBefore<int>(keyValuePair.Key);
            Assert.Equal(2, intsBefore.Count());
            Assert.Contains(2, intsBefore);
            Assert.Contains(4, intsBefore);
        }

        [Fact]
        public void CanCountFactBeforeOtherFact()
        {
            var rule = CreateRule();
            var keyValuePair = rule.FindAllTimed<string>(s => s.Contains("f")).First();
            var intsBeforeCount = rule.CountBefore<int>(keyValuePair.Key);
            Assert.Equal(2, intsBeforeCount);

        }

        [Fact]
        public void CanFindFactsAfter()
        {
            var rule = CreateRule();
            var keyValuePair = rule.FindAllTimed<string>(s => s.Contains("one")).First();
            IEnumerable<int> intAfter= rule.FindAfter<int>(keyValuePair.Key);
            Assert.Equal(2, intAfter.Count());
            Assert.Contains(2, intAfter);
            Assert.Contains(4, intAfter);
        }

        [Fact]
        public void CanCountFactsAfter()
        {
            var rule = CreateRule();
            var keyValuePair = rule.FindAllTimed<string>(s => s.Contains("one")).First();
           int intAfterCount = rule.CountAfter<int>(keyValuePair.Key);
            Assert.Equal(2, intAfterCount);
        }


        private FakeTimedRule CreateRule()
        {
            var ruleRepository = new TimedFactRepository();
            var rule = new FakeTimedRule(ruleRepository);
            ruleRepository.Add("one");
            ruleRepository.Add(2);
            ruleRepository.Add(3.0);
            ruleRepository.Add(4);
            ruleRepository.Add("five");
            return rule;
        }
    }
}
