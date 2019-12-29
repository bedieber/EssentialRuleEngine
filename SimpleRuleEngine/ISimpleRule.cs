namespace SimpleRuleEngine
{
    public interface ISimpleRule
    {
        int Priority { get; }
        bool CanRun(IFactRepository repository);
        bool Run(IFactRepository repository);
    }
}