using System;
using System.Collections.Generic;
using Xunit;

namespace EssentialRules.Test
{
    public class TimedBaseRuleTests
    {

        [Fact]
        public void CanFindFactsBeforeTimestamp()
        {
            
        }
        
        
        private static TimedBaseRule InitRule()
        {
            var rule = new FakeTimedRule(new TimedFactRepository());
            var ruleRepository = new TimedFakeRepository();
            //TODO
            rule.Repository = ruleRepository;
            return rule;
        }
    }

    internal class TimedFakeRepository : IFactRepository
    {
        public IEnumerable<T> FindAll<T>(params Predicate<T>[] func)
        {
            throw new NotImplementedException();
        }

        public void Add(object fact)
        {
            throw new NotImplementedException();
        }

        public void RemoveFact(object fact)
        {
            throw new NotImplementedException();
        }
    }
}