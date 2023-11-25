using Fictoria.Logic.Fact;

namespace Fictoria.Logic.Evaluation;

public class Scope
{
    public IDictionary<string, Type.Type> Types { get; private set; }
    public IDictionary<string, Schema> Schemata { get; private set; }
    public IDictionary<string, ISet<Fact.Fact>> Facts { get; private set; }
    public IDictionary<string, Type.Type> Instances { get; private set; }
    public IDictionary<string, Function.Function> Functions { get; private set; }
    public IDictionary<string, object> Bindings { get; private set; }

    public Scope()
    {
        Types = new Dictionary<string, Type.Type>();
        Schemata = new Dictionary<string, Schema>();
        Facts = new Dictionary<string, ISet<Fact.Fact>>();
        Instances = new Dictionary<string, Type.Type>();
        Functions = new Dictionary<string, Function.Function>();
        Bindings = new Dictionary<string, object>();
    }

    /// <summary>
    /// Shallow clones the facts from <paramref name="scope"/>, but keeps everything else.
    /// </summary>
    /// <param name="scope">The scope from which to clone and copy.</param>
    public Scope(Scope scope)
    {
        Types = scope.Types;
        Schemata = scope.Schemata;
        Instances = scope.Instances;
        Functions = scope.Functions;
        Bindings = scope.Bindings;
        
        var clone = new Dictionary<string, ISet<Fact.Fact>>();
        foreach (var (schema, set) in scope.Facts)
        {
            clone[schema] = new HashSet<Fact.Fact>(set);
        }
        Facts = clone;
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

    public void Merge(Scope other)
    {
        throw new NotImplementedException();
    }
}