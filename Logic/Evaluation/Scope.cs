using Fictoria.Logic.Fact;
using Fictoria.Logic.Parser;
using Fictoria.Logic.Search;

namespace Fictoria.Logic.Evaluation;

public class Scope
{
    public IDictionary<string, Type.Type> Types { get; private set; }
    public IDictionary<Type.Type, ISet<Type.Type>> TypesByParent { get; private set; }
    public IDictionary<string, Schema> Schemata { get; private set; }
    public IDictionary<string, ISet<Fact.Fact>> Facts { get; private set; }
    public ISet<Antifact> Antifacts { get; private set; }
    public IDictionary<string, Type.Type> Instances { get; private set; }
    public IDictionary<string, ISet<string>> InstancesByType { get; private set; }
    public IDictionary<string, Function.Function> Functions { get; private set; }
    public IDictionary<string, object> Bindings { get; private set; }

    public Scope()
    {
        Types = new Dictionary<string, Type.Type>();
        TypesByParent = new Dictionary<Type.Type, ISet<Type.Type>>();
        Schemata = new Dictionary<string, Schema>();
        Facts = new Dictionary<string, ISet<Fact.Fact>>();
        Antifacts = new HashSet<Antifact>();
        Instances = new Dictionary<string, Type.Type>();
        InstancesByType = new Dictionary<string, ISet<string>>();
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
        TypesByParent = scope.TypesByParent;
        Schemata = scope.Schemata;
        Functions = scope.Functions;
        Bindings = scope.Bindings;

        var instancesClone = new Dictionary<string, Type.Type>();
        foreach (var (name, type) in scope.Instances)
        {
            instancesClone[name] = type;
        }
        Instances = instancesClone;

        var instancesByTypeClone = new Dictionary<string, ISet<string>>();
        foreach (var (key, value) in scope.InstancesByType)
        {
            instancesByTypeClone[key] = new HashSet<string>(value);
        }
        InstancesByType = instancesByTypeClone;
        
        var factsClone = new Dictionary<string, ISet<Fact.Fact>>();
        foreach (var (schema, set) in scope.Facts)
        {
            factsClone[schema] = new HashSet<Fact.Fact>(set);
        }
        Facts = factsClone;
        Antifacts = new HashSet<Antifact>();
    }

    public IList<Type.Type> GetAllSubtypes(Type.Type type)
    {
        var results = new List<Type.Type>();

        if (TypesByParent.TryGetValue(type, out var types))
        {
            foreach (var t in types)
            {
                results.Add(t);
                results.AddRange(GetAllSubtypes(t));
            }
        }

        return results;
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

    public void UndefineFact(Fact.Fact fact)
    {
        var schema = fact.Schema.Name;
        Facts[schema].Remove(fact);
    }
    
    public void DefineAntifact(Antifact fact)
    {
        Antifacts.Add(fact);
    }
    
    public void DefineInstance(Instance instance)
    {
        var instances = InstancesByType;
        if (!instances.ContainsKey(instance.Type.Name))
        {
            instances[instance.Type.Name] = new HashSet<string>();
        }

        instances[instance.Type.Name].Add(instance.Name);
        Instances[instance.Name] = instance.Type;
    }

    public void DefineFunction(Function.Function function)
    {
        Functions[function.Name] = function;
    }

    public void Merge(Scope other)
    {
        foreach (var (i, t) in other.Instances)
        {
            DefineInstance(new Instance(i, t));
        }
        
        //TODO
        foreach (var fs in other.Facts)
        {
            foreach (var f in fs.Value)
            {
                DefineFact(f);
            }
        }

        var program = new Program(this);
        var context = new Context(program);
        foreach (var af in other.Antifacts)
        {
            var facts = FactSearch.FindAllRaw(context, Schemata[af.Matcher.Functor], af.Matcher.Arguments);
            foreach (var f in facts)
            {
                UndefineFact(f);
            }
        }
        
        Linker.LinkAll(this);
    }
}