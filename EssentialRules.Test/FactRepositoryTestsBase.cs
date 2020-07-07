using System;
using Xunit;

namespace EssentialRules.Test
{
    public class FactRepositoryTestsBase
    {
        protected Type FactRepositoryType { get; set; }

        [Fact]
        public void CanFindByType()
        {
            var repository = InitRepository();
            Assert.NotEmpty(repository.FindAll<string>());
        }

        [Fact]
        public void CanFindByPredicate()
        {
            var repository= InitRepository();
            var result = repository.FindAll<string>((s) => s.Contains("e"));
            Assert.NotEmpty(result);
        }

        [Fact]
        public void CanFindReturnsEmptyOnTypeMismatch()
        {
            var repository = InitRepository();
            var result = repository.FindAll<long>();
            Assert.Empty(result);
        }

        [Fact]
        public void CanFindReturnsEmptyOnPredicateMismatch()
        {
            var repository = InitRepository();
            var result = repository.FindAll<int>(i => i > 5);
            Assert.Empty(result);
        }

        protected IFactRepository InitRepositoryEmpty()
        {
            var repository = (IFactRepository)FactRepositoryType.Assembly.CreateInstance(FactRepositoryType.FullName);
            return repository;
        }

        private IFactRepository InitRepository()
        {
            var repository = InitRepositoryEmpty();
            repository.Add(new String("test"));
            repository.Add(5);
            repository.Add(23.5);
            return repository;
        }
    }
}