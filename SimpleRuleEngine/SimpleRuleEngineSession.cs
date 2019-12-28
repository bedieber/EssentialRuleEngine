using System;
using System.Collections.Generic;
using System.Threading;

namespace SimpleRuleEngine
{
    public class SimpleRuleEngineSession
    {
        //TODO extract interface
        //TODO how to delete facts after rule is run? Postcondition? immediately?
        private SortedList<int, ISimpleRule> Rules { get; set; }

        internal IFactRepository FactsRepository { get; private set; }

        private Mutex _mutex = new Mutex();

        public SimpleRuleEngineSession()
        {
            Rules = new SortedList<int, ISimpleRule>();
        }

        public void AddRule(ISimpleRule rule)
        {
            lock (_mutex)
            {
                Rules.Add(rule.Priority, rule);
            }
        }

        public void AddFact(object fact)
        {
            lock (_mutex)
            {
                FactsRepository.Add(fact);
            }
        }

        public void RemoveFact()
        {
            throw new NotImplementedException();
        }

        public void Fire()
        {
            lock (_mutex)
            {
                var enumerator = Rules.GetEnumerator();
                do
                {
                    if (enumerator.Current.Value.CanRun())
                    {
                        // TODO handle false as return value
                        // TODO handle exceptions
                        enumerator.Current.Value.Run();
                    }
                } while (enumerator.MoveNext());
            }
        }
    }
}