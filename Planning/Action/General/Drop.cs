using Fictoria.Logic;

namespace Fictoria.Planning.Action.General;

public class DropFactory : ActionFactory<Drop, string>
{
    public static readonly DropFactory Instance = new();
    
    public Drop Create(string input)
    {
        return new Drop(input);
    }

    public IEnumerable<string> Space(Program program)
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
                carrying(_)
                """;
    }

    public override string Effects()
    {
        return $"""
                ~carrying(_).
                """;
    }
}