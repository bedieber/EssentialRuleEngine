using System.Linq;
using EssentialRules.Test.DTO;

namespace EssentialRules.Test.Rules
{
    public class TestRule1 : BaseRule
    {
        internal bool CanRunCalled { get; private set; }
        internal bool RunCalled { get; private set; }
        
        public override bool CanRun()
        {
            CanRunCalled = true;
            TestDTO1 o1 = null;
            if(ExactlyOne<TestDTO1>((b) => o1=b, b => b.Property1 , b => b.Property2 ) &&
            Exists<TestDTO2>(f => f.Property1 == false, f => f.Property2 > 10))
            {
                return true;
            }

            if (FindAll<TestDTO1>().Count() > 2 || FindAll<TestDTO1>(d=>d.Property1).Count()>3)
            {
                return true;
            }

            return false;
        }

        public override bool Run()
        {
            RunCalled = true;
            return true;
        }

        internal void CallRemove(object fact)
        {
            RemoveFact(fact);
        }

        public void PerformAdd(string bla)
        {
            AddFact(bla);
        }
    }
}