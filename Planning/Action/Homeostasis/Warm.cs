using Fictoria.Logic;

namespace Fictoria.Planning.Action.Homeostasis;

public class WarmFactory : ActionFactory<Warm, string>
{
    public static readonly WarmFactory Instance = new();
    
    public Warm Create(string input)
    {
        return new Warm(input);
    }

    public IEnumerable<string> Space(Program program)
    {
        return program.Scope.InstancesByType["fire"];
    }
}

public class Warm : Action
{
    public string Fire { get; }

    public Warm(string fire)
    {
        Fire = fire;
    }

    public override int Cost(Program program)
    {
        return 1;
    }

    public override string Conditions()
    {
        return $"""
                !warm(self) and
                location(fire, _, _)
                """;
    }

    public override string Effects()
    {
        return $"""
                warm(self).
                """;
    }
}