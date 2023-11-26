using Fictoria.Domain.Utilities;
using Fictoria.Logic;
using Type = Fictoria.Logic.Type.Type;

namespace Fictoria.Planning.Action.General;

public class SearchFactory : ActionFactory<Search, string>
{
    public static readonly SearchFactory Instance = new();
    
    public Search Create(string input)
    {
        return new Search(input);
    }

    public IEnumerable<string> Space(Program program)
    {
        var results = program.SearchAll("searchable(_)");
        return results.Select(r => ((Type)r.Arguments[0]).Name);
    }
}

public class Search : Action
{
    public string Type { get; }

    public Search(string type)
    {
        Type = type;
    }

    public override int Cost(Program program)
    {
        return 100;
    }

    public override string Conditions()
    {
        return $"""
               !location({Type}, _, _)
               """;
    }

    public override string Effects()
    {
        var name = $"{Type}_{Identifier.Random()}";
        return $"""
               instance({name}, {Type}).
               location({name}, 0, 0).
               """;
    }
}