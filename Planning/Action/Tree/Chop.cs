using Fictoria.Domain.Utilities;
using Fictoria.Logic;

namespace Fictoria.Planning.Action.Tree;

public class ChopFactory : ActionFactory
{
    public static readonly ChopFactory Instance = new();
    
    public Action Create(object input)
    {
        return new Chop((string)input);
    }

    public IEnumerable<object> Space(Program program)
    {
        if (program.Scope.InstancesByType.TryGetValue("tree", out var found))
        {
            return found;
        }

        return Array.Empty<object>();
    }
}

public class Chop : Action
{
    public string Tree { get; }

    public Chop(string tree)
    {
        Tree = tree;
    }

    public override int Cost(Program program)
    {
        return 20;
    }

    public override string Conditions()
    {
        return $"""
                location({Tree}, _, _) and
                !location(wood, _, _)
                """;
    }

    public override string Effects()
    {
        var name = $"wood_{Identifier.Random()}";
        return $"""
                instance({name}, wood).
                location({name}, 0, 0).
                """;
    }
}