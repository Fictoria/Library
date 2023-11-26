using Fictoria.Domain.Utilities;
using Fictoria.Logic;

namespace Fictoria.Planning.Action.Campfire;

public class LightFactory : ActionFactory
{
    public static readonly LightFactory Instance = new();
    
    public Action Create(object input)
    {
        return new Light((string)input);
    }

    public IEnumerable<object> Space(Program program)
    {
        if (program.Scope.InstancesByType.TryGetValue("campfire", out var found))
        {
            return found;
        }

        return Array.Empty<object>();
    }
}

public class Light : Action
{
    public string Campfire { get; }

    public Light(string campfire)
    {
        Campfire = campfire;
    }

    public override int Cost(Program program)
    {
        return 2;
    }

    public override string Conditions()
    {
        return $"""
                location(@cf, _, _) and //TODO need some way of constraining types; maybe use :: infix search
                contains(cf, wood)
                """;
    }

    public override string Effects()
    {
        var name = $"fire_{Identifier.Random()}";
        return $"""
                instance({name}, fire).
                location({name}, 0, 0).
                """;
    }
}