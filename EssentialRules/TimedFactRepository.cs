using System;
using System.Collections.Generic;
using System.Linq;

namespace EssentialRules
{
    public class TimedFactRepository : IFactRepository
    {
        private Dictionary<DateTime, object> _facts = new Dictionary<DateTime, object>();

        /// <summary>
        /// Implements the default FindAll function disregarding the time aspect
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> FindAll<T>(params Predicate<T>[] func)
        {
            lock(this)
            {
                return _facts.Where(f=>f.Value is T && func.All(fu=>fu((T)f.Value))).Select(f=>(T)f.Value);
            }
        }

        public void Add(object fact)
        {
            lock (this)
            {
                _facts.Add(DateTime.Now, fact);
            }
        }

        public void RemoveFact(object fact)
        {
            lock (this)
            {
                if (_facts.ContainsValue(fact))
                    _facts.Remove(_facts.First(f => f.Value == fact).Key);
            }
        }

        /// <summary>
        /// Additional function that returns timestamped keyvaluepairs
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<DateTime, T>> FindAllTimed<T>(params Predicate<T>[] func) where T: class
        {
            lock(this)
            {
                return _facts.Where(f=>f.Value is T && func.All(fu=>fu((T)f.Value))).Select(pair => new KeyValuePair<DateTime, T>(pair.Key, (T)pair.Value));
            }
        }
    }
}