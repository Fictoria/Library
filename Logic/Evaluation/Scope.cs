using Fictoria.Logic.Fact;

namespace Fictoria.Logic.Evaluation;

public class Scope
{
    public IDictionary<string, Type.Type> Types { get; }
    public IDictionary<string, Schema> Schemata { get; }
    public IDictionary<string, ISet<Fact.Fact>> Facts { get; }
    public IDictionary<string, Type.Type> Instances { get; }
    public IDictionary<string, Function.Function> Functions { get; }
    public IDictionary<string, object> Bindings { get; }
    
    public Scope()
    {
        Types = new Dictionary<string, Type.Type>();
        Schemata = new Dictionary<string, Schema>();
        Facts = new Dictionary<string, ISet<Fact.Fact>>();
        Instances = new Dictionary<string, Type.Type>();
        Functions = new Dictionary<string, Function.Function>();
        Bindings = new Dictionary<string, object>();
    }
}