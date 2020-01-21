namespace EssentialRules
{
    public abstract class TimedBaseRule: BaseRule
    {

        public TimedBaseRule(TimedFactRepository repository)
        {
            Repository = repository;
        }
        
        
        
    }
}