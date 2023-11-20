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
    
    public void DefineType(Type.Type type)
    {
        Types[type.Name] = type;
    }
    
    public void DefineSchema(Schema schema)
    {
        Schemata[schema.Name] = schema;
    }
    
    public void DefineFact(Fact.Fact fact)
    {
        var facts = Facts;
        if (!facts.ContainsKey(fact.Schema.Name))
        {
            facts[fact.Schema.Name] = new HashSet<Fact.Fact>();
        }
        facts[fact.Schema.Name].Add(fact);
    }
    
    public void DefineInstance(Instance instance)
    {
        Instances[instance.Name] = instance.Type;
    }

    public void DefineFunction(Function.Function function)
    {
        Functions[function.Name] = function;
    }
}