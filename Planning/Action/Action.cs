namespace Planning.Actions;

public interface Action
{
    public IEnumerable<string> Preconditions { get; }
    public IEnumerable<Transform>
}