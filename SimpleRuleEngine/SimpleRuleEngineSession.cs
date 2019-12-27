using System.Collections.Generic;

namespace SimpleRuleEngine
{
    public class SimpleRuleEngineSession
    {
        internal SortedList<int, ISimpleRule> Rules { get; private set; }
        
        internal IFactRepository FactsRepository { get; private set; }


        public SimpleRuleEngineSession()
        {
            Rules = new SortedList<int, ISimpleRule>();
        }

        public void AddRule(ISimpleRule rule)
        {
            Rules.Add(rule.Priority, rule);
        }
    }
}