using System;
using Xunit;

namespace EssentialRules.Test
{
    public class FactRepositoryTestsBase
    {
        protected Type FactRepositoryType { get; set; }

        [Theory]
        [InlineData(typeof(TimedFactRepository))]
        [InlineData(typeof(EssentialFactRepository))]
        public void CanFindByType(Type repositoryType)
        {
            var repository = InitRepository(repositoryType);
            Assert.NotEmpty(repository.FindAll<string>());
        }

        [Theory]
        [InlineData(typeof(TimedFactRepository))]
        [InlineData(typeof(EssentialFactRepository))]
        public void CanFindByPredicate(Type repositoryType)
        {
            var repository= InitRepository(repositoryType);
            var result = repository.FindAll<string>((s) => s.Contains("e"));
            Assert.NotEmpty(result);
        }
        
        [Theory]
        [InlineData(typeof(TimedFactRepository))]
        [InlineData(typeof(EssentialFactRepository))]
        public void CanFindReturnsEmptyOnTypeMismatch(Type repositoryType)
        {
            var repository = InitRepository(repositoryType);
            var result = repository.FindAll<long>();
            Assert.Empty(result);
        }
        
        [Theory]
        [InlineData(typeof(TimedFactRepository))]
        [InlineData(typeof(EssentialFactRepository))]
        public void CanFindReturnsEmptyOnPredicateMismatch(Type repositoryType)
        {
            var repository = InitRepository(repositoryType);
            var result = repository.FindAll<int>(i => i > 5);
            Assert.Empty(result);
        }

        protected IFactRepository InitRepositoryEmpty(Type repositoryType)
        {
            var repository = (IFactRepository)repositoryType.Assembly.CreateInstance(repositoryType.FullName ?? throw new NullReferenceException());
            return repository;
        }

        private IFactRepository InitRepository(Type repositoryType)
        {
            var repository = InitRepositoryEmpty(repositoryType);
            repository.Add(new String("test"));
            repository.Add(5);
            repository.Add(23.5);
            return repository;
        }
    }
}