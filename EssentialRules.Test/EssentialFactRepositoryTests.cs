using System;
using Xunit;

namespace EssentialRules.Test
{
    public class EssentialFactRepositoryTests
    {
        
        
        [Fact]
        public void CanAddFact()
        {
            EssentialFactRepository repository=new EssentialFactRepository();
            repository.Add(new String("test"));
            Assert.Single(repository.Repository);
        }

        [Fact]
        public void CanRemoveFact()
        {
            EssentialFactRepository repository=new EssentialFactRepository();
            var fact = new String("test");
            repository.Add(fact);
            repository.RemoveFact(fact);
            Assert.Empty(repository.Repository);
        }

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
        
        
        private static EssentialFactRepository InitRepository()
        {
            EssentialFactRepository repository = new EssentialFactRepository();
            repository.Add(new String("test"));
            repository.Add(5);
            repository.Add(23.5);
            return repository;
        }
    }

}