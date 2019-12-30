using System;
using System.Collections.Generic;
using System.Threading;

namespace EssentialRules
{
    public class EssentialRulesSession
    {
        //TODO extract interface
        private SortedList<int, IRule> Rules { get; set; }

        internal IFactRepository FactsRepository { get; set; }

        private Mutex _mutex = new Mutex();

        public EssentialRulesSession()
        {
            Rules = new SortedList<int, IRule>();
            FactsRepository=new EssentialFactRepository();
        }

        public void AddRule(IRule rule)
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

        public void RemoveFact(object fact)
        {
            FactsRepository.RemoveFact(fact);
        }

        public void Fire()
        {
            lock (_mutex)
            {
                using (var enumerator = Rules.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Current.Value.CanRun(FactsRepository))
                        {
                            // TODO handle false as return value
                            // TODO handle exceptions
                            enumerator.Current.Value.Run(FactsRepository);
                        }
                    }
                }
            }
        }
    }
}