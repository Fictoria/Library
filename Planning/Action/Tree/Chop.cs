using Fictoria.Domain.Utilities;
using Fictoria.Logic;

namespace Fictoria.Planning.Action.Tree;

public class ChopFactory : ActionFactory<Chop, string>
{
    public static readonly ChopFactory Instance = new();
    
    public Chop Create(string input)
    {
        return new Chop(input);
    }

    public IEnumerable<string> Space(Program program)
    {
        return program.Scope.InstancesByType["tree"];
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
                location({Tree}, _, _)
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