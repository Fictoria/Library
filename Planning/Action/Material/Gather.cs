using Fictoria.Logic;

namespace Fictoria.Planning.Action.Material;

public class GatherFactory : ActionFactory
{
    public static readonly GatherFactory Instance = new();
    
    public Action Create(object input)
    {
        return new Gather((string)input);
    }

    public IEnumerable<object> Space(Program program)
    {
        var material = program.Scope.Types["material"];
        var materials = program.Scope.GetAllSubtypes(material).Select(m => m.Name);
        
        return materials;
    }
}

public class Gather : Action
{
    public string Material { get; }

    public Gather(string material)
    {
        Material = material;
    }
    
    public override int Cost(Program program)
    {
        //TODO distance
        return 2;
    }

    public override string Conditions()
    {
        return $"""
                !carrying(_) and
                location({Material}, _, _)
                """;
    }

    public override string Effects()
    {
        return $"""
                carrying({Material}).
                ~location(wood, _, _).
                """;
    }

    public override string ToString()
    {
        return $"gather({Material})";
    }
}