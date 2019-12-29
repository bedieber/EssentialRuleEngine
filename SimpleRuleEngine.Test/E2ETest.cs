using System;
using SimpleRuleEngine.Test.DTO;
using SimpleRuleEngine.Test.Rules;
using Xunit;

namespace SimpleRuleEngine.Test
{
    public class E2ETest
    {
        [Fact]
        public void Test1()
        {
            SimpleRuleEngineSession session=new SimpleRuleEngineSession();
            session.FactsRepository=new FakeRepository();
            session.AddFact(new TestDTO1{Property1 = true, Property2 = true});
            session.AddFact(new TestDTO2{Property1 = false, Property2 = 11});
            
            TestRule1 rule=new TestRule1();
            session.AddRule(rule);
            session.Fire();
            
            Assert.True(rule.CanRunCalled);
            Assert.True(rule.RunCalled);
        }
    }
}