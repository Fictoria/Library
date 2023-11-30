using Fictoria.Domain.Utilities;
using Fictoria.Logic;
using Type = Fictoria.Logic.Type.Type;

namespace Fictoria.Planning.Action.General;

public class SearchFactory : ActionFactory
{
    public static readonly SearchFactory Instance = new();
    
    public Action Create(object input)
    {
        return new Search((string)input);
    }

    public IEnumerable<object> Space(Program program)
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
        return 1000;
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

    public override IEnumerable<string> Terms()
    {
        return new[] { "search", "location", Type };
    }

    public override string ToString()
    {
        return $"search({Type})";
    }
}