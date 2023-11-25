namespace Fictoria.Logic.Search;

public class FactResult
{
    public string Name { get; }
    public IList<object> Arguments { get; }

    public FactResult(string name, IList<object> arguments)
    {
        Name = name;
        Arguments = arguments;
    }
}