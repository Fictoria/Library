using Fictoria.Logic;

namespace Fictoria.Planning.Action.General;

public class DropFactory : ActionFactory
{
    public static readonly DropFactory Instance = new();
    
    public Action Create(object input)
    {
        return new Drop((string)input);
    }

    public IEnumerable<object> Space(Program program)
    {
        var results = program.SearchAll("carrying(_)");
        return results.Select(r => ((Type)r.Arguments[0]).Name);
    }
}

public class Drop : Action
{
    public string Thing { get; }

    public Drop(string thing)
    {
        Thing = thing;
    }
    
    public override int Cost(Program program)
    {
        return 1;
    }

    public override string Conditions()
    {
        return $"""
                carrying({Thing})
                """;
    }

    public override string Effects()
    {
        return $"""
                ~carrying({Thing}).
                """;
    }

    public override string ToString()
    {
        return $"drop({Thing})";
    }
}