using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace EssentialRules.Test
{
    public class FakeRepository : IFactRepository
    {
        internal List<object> Repository = new List<object>();

        public IEnumerable<T> FindAll<T>(params Predicate<T>[] func)
        {
            var ofType = Repository.OfType<T>();
            foreach (var predicate in func)
            {
                ofType = ofType.Intersect(ofType.Where(t => predicate(t)));
            }

            return ofType.ToImmutableList();
        }

        public void Add(object fact)
        {
            Repository.Add(fact);
        }

        public void RemoveFact(object fact)
        {
            Repository.Remove(fact);
        }
    }
}