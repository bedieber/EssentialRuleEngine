namespace SimpleRuleEngine
{
    public interface ISimpleRule
    {
        int Priority { get; }
        bool CanRun();
        bool Run();
    }
}