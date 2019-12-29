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
        /// Defaults to 1.
        /// </summary>
        public int Priority { get; set; } = 1;

        public bool CanRun(IFactRepository repository)
        {
            Repository = repository;
            return CanRun();
        }

        public bool Run(IFactRepository repository)
        {
            Repository = repository;
            return Run();
        }

        protected void RemoveFact(object fact)
        {
            Repository.RemoveFact(fact);
        }

        /// <summary>
        /// This method determines if all conditions of a rule are satisfied for the rule to be executed.<br />
        /// If the method returns true, <see cref="Run"/> will be called.
        /// </summary>
        /// <returns><c>true</c> if all conditions of the rule are satisfied</returns>
        public abstract bool CanRun();

        /// <summary>
        /// In this method, the rule's action is executed
        /// </summary>
        /// <returns><c>true</c> if run was successful.</returns>
        public abstract bool Run();

        /// <summary>
        /// Looks for all occurances of type <typeparamref name="T"/> objects in the facts base that satisfy all conditions defined with <paramref name="predicates"/>.
        /// </summary>
        /// <param name="predicates">Predicate functions that determine if an object of type <typeparamref name="T"/> in the facts base is chosen.</param>
        /// <typeparam name="T">Type of object to look for in the facts base</typeparam>
        /// <returns>An IEnumerable of <typeparamref name="T"/> satisfying all conditions.</returns>
        protected internal IEnumerable<T> FindAll<T>(params Predicate<T>[] predicates)
        {
            return Repository.FindAll<T>(predicates);
        }

        /// <summary>
        /// Determines if objects of type <typeparamref name="T"/> that satisfy the conditions defined with <paramref name="predicates"/> exist.
        /// </summary>
        /// <param name="predicates">Predicate functions that determine if an object of type <typeparamref name="T"/> in the facts base is chosen.</param>
        /// <typeparam name="T">Type of object to look for in the facts base</typeparam>
        /// <returns><c>true</c> if at least one matching item is found in the facts base.</returns>
        protected internal bool Exists<T>(params Predicate<T>[] predicates)
        {
            return FindAll<T>(predicates).Any();
        }

        /// <summary>
        /// Determines if exactly one item of type <typeparamref name="T"/> exists in the facts base that satisfies the conditions defined in <paramref name="predicates"/>
        /// </summary>
        /// <param name="assignment">An action to perform on the resulting item, e.g., assignment to a local variable in the rule.</param>
        /// <param name="predicates">Predicate functions that determine if an object of type <typeparamref name="T"/> in the facts base is chosen.</param>
        /// <typeparam name="T">Type of item to look for in the facts base</typeparam>
        /// <returns><c>true</c> if exactly one item is found, <c>false</c> if zero or more than one are present.</returns>
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

        /// <summary>
        /// Counts the number of items in the facts base that are of type <typeparamref name="T"/> and satisfy the conditions defined with <paramref name="predicates"/> 
        /// </summary>
        /// <param name="predicates">Predicate functions that determine if an object of type <typeparamref name="T"/> in the facts base is chosen.</param>
        /// <typeparam name="T">Type of item to look for in the facts base</typeparam>
        /// <returns>The number of items present in the facts base.</returns>
        protected internal int Count<T>(params Predicate<T>[] predicates)
        {
            return FindAll<T>(predicates).Count();
        }
    }
}