using System.Linq;
using Xunit;

namespace EssentialRules.Test
{
    public class TimedFactRepositoryTests : FactRepositoryTests
    {

        public TimedFactRepositoryTests()
        {
            FactRepositoryType = typeof(TimedFactRepository);
        }


        [Fact]
        public void CanRetrieveTimedFactsSortedByTimestamp()
        {
            TimedFactRepository repository = (TimedFactRepository)InitRepositoryEmpty();
            repository.Add(1);
            repository.Add(2);
            repository.Add(3);
            var timed = repository.FindAllTimed<int>();
            Assert.True(timed.First().Value==1);
            Assert.True(timed.Last().Value==3);
        }

        [Fact]
        public void CanRetrieveTimedFactsInHeterogeneousFactsbaseSortedByTimestamp()
        {
            TimedFactRepository repository = (TimedFactRepository)InitRepositoryEmpty();
            repository.Add(1);
            repository.Add("First");
            repository.Add(2);
            repository.Add(3);
            repository.Add("Second");
            
            var timed = repository.FindAllTimed<string>();
            Assert.True(timed.First().Key<timed.Last().Key);
        }

        [Fact]
        public void CanRetrieveTimedFactsWithPredicatesSortedByTimestamp()
        {
            TimedFactRepository repository = (TimedFactRepository)InitRepositoryEmpty();
            repository.Add(1);
            string s1 = "stringFirst";
            string s2 = "stringSecond";
            repository.Add(s1);
            repository.Add(2);
            repository.Add("third");
            repository.Add(3);
            repository.Add(s2);

            var allTimed = repository.FindAllTimed<string>(s => s.Contains("string"));
            Assert.Equal(2,allTimed.Count());
            Assert.True(allTimed.First().Value.Contains("First"));
        }
    }
}