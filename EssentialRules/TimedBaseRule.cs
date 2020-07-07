using System;
using System.Collections.Generic;
using System.Linq;

namespace EssentialRules
{
    public abstract class TimedBaseRule: BaseRule
    {

        public TimedBaseRule(TimedFactRepository repository)
        {
            Repository = repository;
        }


        protected internal IEnumerable<T> FindBefore<T>(DateTime timestamp)
        {
            var allTimed = ((TimedFactRepository) Repository).FindAllTimed<T>();
            return allTimed.TakeWhile(i => i.Key < timestamp).Select(k=>k.Value);
        }
        
        public IEnumerable<T> FindAfter<T>(DateTime timestamp)
        {
            var allTimed = ((TimedFactRepository) Repository).FindAllTimed<T>();
            return allTimed.SkipWhile(k => k.Key <= timestamp).Select(k=>k.Value);
        }

        public IEnumerable<KeyValuePair<DateTime, T>> FindAllTimed<T>(Predicate<T> func)
        {
            return ((TimedFactRepository) Repository).FindAllTimed(func);
        }

        public int CountBefore<T>(DateTime key)
        {
            return FindBefore<T>(key).Count();
        }


        public int CountAfter<T>(DateTime timestamp)
        {
            return FindAfter<T>(timestamp).Count();
        }
    }
}