using System;
using System.Collections.Generic;
using System.Linq;

namespace EssentialRules
{
    public class EssentialFactRepository:IFactRepository
    {
        internal List<object> Repository = new List<object>();

        public IEnumerable<T> FindAll<T>(params Predicate<T>[] func)
        {
            var ofType = Repository.OfType<T>();
            foreach (var predicate in func)
            {
                var enumerable = ofType.ToList();
                ofType = enumerable.Intersect(enumerable.Where(t => predicate(t)));
            }

            //TODO immutable?
            return ofType;
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