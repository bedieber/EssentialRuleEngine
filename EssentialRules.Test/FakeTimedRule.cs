namespace EssentialRules.Test
{
    public class FakeTimedRule : TimedBaseRule
    {
        public FakeTimedRule(TimedFactRepository repository) : base(repository)
        {
        }

        public override bool CanRun()
        {
            return true;
        }

        public override bool Run()
        {
            return true;
        }
    }
}