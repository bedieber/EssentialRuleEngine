using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SimpleRuleEngine.Test
{
    public class FakeRepository : IFactRepository
    {
        internal List<object> _repository = new List<object>();

        public IEnumerable<T> FindAll<T>(params Predicate<T>[] func)
        {
            var ofType = _repository.OfType<T>();
            foreach (var predicate in func)
            {
                ofType = ofType.Intersect(ofType.Where(t => predicate(t)));
            }

            return ofType.ToImmutableList();
        }

        public void Add(object fact)
        {
            _repository.Add(fact);
        }

        public void RemoveFact(object fact)
        {
            _repository.Remove(fact);
        }
    }
}