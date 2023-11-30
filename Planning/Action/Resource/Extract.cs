using Fictoria.Domain.Utilities;
using Fictoria.Logic;
using Fictoria.Planning.Utilities;

namespace Fictoria.Planning.Action.Resource;

public class ExtractFactory : ActionFactory
{
    public static readonly ExtractFactory Instance = new();
    
    public Action Create(object input)
    {
        return new Extract((Pair<string, string>)input);
    }

    public IEnumerable<object> Space(Program program)
    {
        var resource = program.Scope.Types["resource"];
        var material = program.Scope.Types["material"];
        var resources = program.Scope.GetAllSubtypes(resource).Select(r => r.Name);
        var materials = program.Scope.GetAllSubtypes(material).Select(m => m.Name);
        
        return resources.CartesianProduct(materials);
    }
}

public class Extract : Action
{
    public string Resource { get; }
    public string Material { get; }

    public Extract(Pair<string, string> input)
    {
        Resource = input.First;
        Material = input.Second;
    }

    public override int Cost(Program program)
    {
        return 20;
    }

    public override string Conditions()
    {
        return $"""
                provides({Resource}, {Material}) and
                location({Resource}, _, _) and
                !location({Material}, _, _)
                """;
    }

    public override string Effects()
    {
        var name = $"{Material.Substring(0, 4).ToLower()}_{Identifier.Random()}";
        return $"""
                instance({name}, {Material}).
                location({name}, 0, 0).
                """;
    }

    public override string ToString()
    {
        return $"extract({Resource}, {Material})";
    }
}