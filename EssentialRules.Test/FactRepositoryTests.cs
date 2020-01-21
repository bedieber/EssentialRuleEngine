using System;
using Xunit;

namespace EssentialRules.Test
{
    public class FactRepositoryTests : FactRepositoryTestsBase
    {
        public FactRepositoryTests()
        {
            FactRepositoryType = typeof(EssentialFactRepository);
        }
        
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
    }

}