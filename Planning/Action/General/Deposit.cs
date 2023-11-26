using Fictoria.Logic;
using Fictoria.Planning.Utilities;

namespace Fictoria.Planning.Action.General;

public class DepositFactory : ActionFactory
{
    public static readonly DepositFactory Instance = new();
    
    public Action Create(object input)
    {
        return new Deposit((Pair<string, string>)input);
    }

    public IEnumerable<object> Space(Program program)
    {
        //TODO yikes
        var building = program.Scope.Types["building"];
        var material = program.Scope.Types["material"];
        var buildings = program.Scope.GetAllSubtypes(building).Select(b => b.Name);
        var materials = program.Scope.GetAllSubtypes(material).Select(m => m.Name);
        
        return buildings.CartesianProduct(materials);
    }
}

public class Deposit : Action
{
    public string Building { get; }
    public string Material { get; }

    public Deposit(Pair<string, string> both)
    {
        Building = both.First;
        Material = both.Second;
    }

    public override int Cost(Program program)
    {
        return 1;
    }

    public override string Conditions()
    {
        return $"""
                location({Building}, _, _) and
                consumes({Building}, {Material}) and
                !contains({Building}, {Material}) and
                carrying({Material})
                """;
    }

    public override string Effects()
    {
        return $"""
                contains({Building}, {Material}).
                ~carrying(_).
                """;
    }
}