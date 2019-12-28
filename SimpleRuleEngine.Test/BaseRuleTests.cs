using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleRuleEngine.Test.Rules;
using Xunit;
using Xunit.Sdk;

namespace SimpleRuleEngine.Test
{
    public class BaseRuleTests
    {
        [Fact]
        public void FindAllWithoutPredicateReturnsAllValuesOfCorrectType()
        {
            var rule = InitRule();

            var findAll = rule.FindAll<int>();
            Assert.Equal(3, findAll.Count());
        }

        [Fact]
        public void FindallWithoutPredicateReturnsEmptyEnumerableForNonexistingType()
        {
            var rule = InitRule();
            var result = rule.FindAll<double>();
            Assert.Empty(result);
        }

        [Fact]
        public void FindAllWithPredicateReturnsSubsetOfRepository()
        {
            var rule = InitRule();
            var result = rule.FindAll<int>(i => i > 1);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void FindAllWithTwoPredicateReturnsSubsetOfRepository()
        {
            var rule = InitRule();
            var result = rule.FindAll<int>(i => i > 1, i => i > 2);
            Assert.Single(result);
        }

        [Fact]
        public void ExactlyOneWithoutParametersReturnsTrueForSingleItem()
        {
            var rule = InitRule();
            Assert.True(rule.ExactlyOne<string>());
        }

        [Fact]
        public void ExactlyOneWithPredicatesFindsExactItem()
        {
            var rule = InitRule();
            Assert.True(rule.ExactlyOne<int>(null, i => i == 1));
        }

        [Fact]
        public void ExactlyOneWithPredicatesFindsExactItemAndAssignsItCorrectly()
        {
            var rule = InitRule();
            int value = -1;
            Assert.True(rule.ExactlyOne<int>((i) => value = i, i => i == 1));
            Assert.Equal(1, value);
        }
        
        [Fact]
        public void ExactlyOneWithoutParametersAssignsCorrectValue()
        {
            var rule = InitRule();
            string value = null;
            Assert.True(rule.ExactlyOne<string>(s=>value=s));
            Assert.Equal("Hello", value);
        }

        [Fact]
        public void CountReturnsZeroForNonExistingItems()
        {
            var rule = InitRule();
            Assert.Equal(0,rule.Count<bool>());
        }

        [Fact]
        public void CountReturnsCorrectNumberWithoutPredicate()
        {
            var rule = InitRule();
            Assert.Equal(3, rule.Count<int>());
        }

        [Fact]
        public void CountReturnsCorrectNumberWithPredicate()
        {
            var rule = InitRule();
            Assert.Equal(2, rule.Count<int>(i=>i>1));
        }

        private static BaseRule InitRule()
        {
            BaseRule rule = new TestRule1();
            var ruleRepository = new FakeRepository();
            ruleRepository._repository.AddRange(new object[] {1, 2, 3, "Hello"});
            rule.Repository = ruleRepository;
            return rule;
        }
    }

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
    }
}