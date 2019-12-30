using EssentialRules.Test.DTO;
using EssentialRules.Test.Rules;
using Xunit;

namespace EssentialRules.Test
{
    public class E2ETest
    {
        [Fact]
        public void Test1()
        {
            EssentialRulesSession session=new EssentialRulesSession();
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