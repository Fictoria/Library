using Fictoria.Logic;

namespace Fictoria.Planning.Action.Homeostasis;

public class WarmFactory : ActionFactory
{
    public static readonly WarmFactory Instance = new();
    
    public Action Create(object input)
    {
        return new Warm((string)input);
    }

    public IEnumerable<object> Space(Program program)
    {
        if (program.Scope.InstancesByType.TryGetValue("fire", out var found))
        {
            return found;
        }

        return Array.Empty<object>();
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

    public override string ToString()
    {
        return $"warm({Fire})";
    }
}