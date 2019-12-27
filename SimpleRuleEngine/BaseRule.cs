using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleRuleEngine
{
    /// <summary>
    /// Baser implementation of ISimpleRule. Provides basis methods for facts base querying
    /// </summary>
    public abstract class BaseRule : ISimpleRule
    {
        internal IFactRepository Repository { get; set; }


        /// <summary>
        /// Rule priority. The higher, the earlier the rule is executed. 
        /// </summary>
        public int Priority { get; set; }

        internal bool CanRun(IFactRepository repository)
        {
            Repository = repository;
            return CanRun();
        }
        public abstract bool CanRun();

        public abstract bool Run();

        protected internal IEnumerable<T> FindAll<T>(params Predicate<T>[] predicates)
        {
            return Repository.FindAll<T>(predicates);
        }

        protected internal IEnumerable<T> FindAll<T>()
        {
            return FindAll<T>(t => true);
        }

        protected internal bool Exists<T>(params Predicate<T>[] predicates)
        {
            return FindAll<T>(predicates).Any();
        }

        protected internal bool ExactlyOne<T>(System.Action<T> assignment=null, params Predicate<T>[] predicates)
        {
            var objects = FindAll<T>(predicates);
            if (!objects.Any() || objects.Count() > 1)
            {
                return false;
            }

            assignment?.Invoke(objects.First());
            return true;
        }

        protected internal int Count<T>(params Predicate<T>[] predicates)
        {
            return FindAll<T>(predicates).Count();
        }
    }
}