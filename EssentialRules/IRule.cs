namespace EssentialRules
{
    public interface IRule
    {
        int Priority { get; }
        bool CanRun(IFactRepository repository);
        bool Run(IFactRepository repository);
    }
}