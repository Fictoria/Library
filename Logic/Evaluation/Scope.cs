using Fictoria.Domain.Locality;
using Fictoria.Logic.Fact;
using Fictoria.Logic.Index;
using Fictoria.Logic.Parser;
using Fictoria.Logic.Search;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Evaluation;

public class Scope
{
    public IDictionary<string, Type.Type> Types { get; }
    public IDictionary<Type.Type, ISet<Type.Type>> TypesByParent { get; }
    public IDictionary<string, Schema> Schemata { get; }
    public IDictionary<string, SpatialIndex> SpatialIndices { get; }
    public IDictionary<string, ISet<Fact.Fact>> Facts { get; }
    public ISet<Antifact> Antifacts { get; }
    public IDictionary<string, Instance> Instances { get; }
    public IDictionary<string, ISet<string>> InstancesByType { get; }
    public IDictionary<string, Function.Function> Functions { get; }
    public IDictionary<string, Action.Action> Actions { get; }
    public IDictionary<string, object> Bindings { get; }
    public IList<Query> Queries { get; }

    public Scope()
    {
        Types = new Dictionary<string, Type.Type>();
        TypesByParent = new Dictionary<Type.Type, ISet<Type.Type>>();
        Schemata = new Dictionary<string, Schema>();
        SpatialIndices = new Dictionary<string, SpatialIndex>();
        Facts = new Dictionary<string, ISet<Fact.Fact>>();
        Antifacts = new HashSet<Antifact>();
        Instances = new Dictionary<string, Instance>();
        InstancesByType = new Dictionary<string, ISet<string>>();
        Functions = new Dictionary<string, Function.Function>();
        Actions = new Dictionary<string, Action.Action>();
        Bindings = new Dictionary<string, object>();
        Queries = new List<Query>();
    }

    /// <summary>
    ///     Shallow clones the facts from <paramref name="scope" />, but keeps everything else.
    /// </summary>
    /// <param name="scope">The scope from which to clone and copy.</param>
    public Scope(Scope scope)
    {
        Types = scope.Types;
        TypesByParent = scope.TypesByParent;
        Schemata = scope.Schemata;
        Functions = scope.Functions;
        Bindings = scope.Bindings;
        Actions = scope.Actions;

        var instancesClone = new Dictionary<string, Instance>();
        foreach (var (name, instance) in scope.Instances)
        {
            instancesClone[name] = new Instance(instance);
        }
        Instances = instancesClone;

        var instancesByTypeClone = new Dictionary<string, ISet<string>>();
        foreach (var (key, value) in scope.InstancesByType)
        {
            instancesByTypeClone[key] = new HashSet<string>(value);
        }
        InstancesByType = instancesByTypeClone;

        var spatialIndicesClone = new Dictionary<string, SpatialIndex>();
        foreach (var (key, value) in scope.SpatialIndices)
        {
            spatialIndicesClone[key] = new SpatialIndex(value);
        }
        SpatialIndices = spatialIndicesClone;

        var factsClone = new Dictionary<string, ISet<Fact.Fact>>();
        foreach (var (schema, set) in scope.Facts)
        {
            factsClone[schema] = new HashSet<Fact.Fact>(set.Select(f => new Fact.Fact(f)));
        }

        Facts = factsClone;
        Antifacts = new HashSet<Antifact>();
        Queries = new List<Query>();
    }

    public void Resolve(Context context)
    {
        var originalInstances = new Dictionary<string, Instance>(Instances);
        // TODO this function is definitely incomplete
        foreach (var (_, instance) in originalInstances)
        {
            var previousName = instance.Name;
            var previousType = instance.Type;
            instance.Resolve(context);
            if (instance.Name != previousName)
            {
                Instances.Remove(previousName);
                Instances[instance.Name] = instance;
            }

            if (!instance.Type.Equals(previousType))
            {
                InstancesByType[previousType.Name].Remove(previousName);
                if (!InstancesByType.ContainsKey(instance.Type.Name))
                {
                    InstancesByType[instance.Type.Name] = new HashSet<string>();
                }

                InstancesByType[instance.Type.Name].Add(instance.Name);
            }

            if (instance.Type is TypePlaceholder type)
            {
                if (context.ResolveType(type.Name, out var found))
                {
                    // NOTE: this is also linking basically
                    instance.Type = found;
                }
            }
        }

        foreach (var (schema, facts) in Facts)
        {
            foreach (var fact in facts)
            {
                foreach (var arg in fact.Arguments)
                {
                    Resolution.Resolve(context, arg);
                }
            }
        }
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

    public void DefineSchema(Schema schema, SpatialIndex? spatialIndex = null)
    {
        Schemata[schema.Name] = schema;
        if (spatialIndex is not null)
        {
            SpatialIndices[schema.Name] = spatialIndex;
        }
    }

    public void DefineFact(Fact.Fact fact)
    {
        var facts = Facts;
        if (!facts.ContainsKey(fact.Schema.Name))
        {
            facts[fact.Schema.Name] = new HashSet<Fact.Fact>();
        }
        facts[fact.Schema.Name].Add(fact);

        var schema = fact.Schema;
        if (fact.Schema is SchemaPlaceholder placeholder)
        {
            schema = Schemata[placeholder.Name];
        }
        
        if (SpatialIndices.TryGetValue(schema.Name, out var index))
        {
            var context = new Context(new Program(this));
            var idIdx = schema.Parameters.FindIndex(p => p.Name == index.IdField);
            var xIdx = schema.Parameters.FindIndex(p => p.Name == index.XField);
            var yIdx = schema.Parameters.FindIndex(p => p.Name == index.YField);
            var id = fact.Arguments[idIdx].Evaluate(context).ToString()!;
            var x = (double)fact.Arguments[xIdx].Evaluate(context);
            var y = (double)fact.Arguments[yIdx].Evaluate(context);
            index.Insert(id, new Point(x, y), fact);
        }
    }

    public void UndefineFact(Fact.Fact fact)
    {
        var schema = fact.Schema.Name;
        Facts[schema].Remove(fact);

        if (SpatialIndices.TryGetValue(fact.Schema.Name, out var index))
        {
            var context = new Context(new Program(this));
            var idIdx = fact.Schema.Parameters.FindIndex(p => p.Name == index.IdField);
            var id = fact.Arguments[idIdx].Evaluate(context).ToString()!;
            index.Remove(id);
        }
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
        Instances[instance.Name] = instance;
    }

    public void DefineFunction(Function.Function function)
    {
        Functions[function.Name] = function;
    }

    public void DefineAction(Action.Action action)
    {
        Actions[action.Name] = action;
    }

    public void DefineQuery(Query query)
    {
        Queries.Add(query);
    }

    public void Merge(Scope other)
    {
        foreach (var (_, t) in other.Types)
        {
            DefineType(t);
        }

        foreach (var (_, s) in other.Schemata)
        {
            other.SpatialIndices.TryGetValue(s.Name, out var spatialIndex);
            DefineSchema(s, spatialIndex);
        }
        // TODO functions and actions

        foreach (var (_, f) in other.Functions)
        {
            DefineFunction(f);
        }

        foreach (var (_, a) in other.Actions)
        {
            DefineAction(a);
        }

        foreach (var (i, t) in other.Instances)
        {
            DefineInstance(t);
        }

        // undefine first
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

        // TODO
        foreach (var fs in other.Facts)
        {
            foreach (var f in fs.Value)
            {
                DefineFact(f);
            }
        }

        foreach (var (k, v) in other.TypesByParent)
        {
            if (!TypesByParent.ContainsKey(k))
            {
                TypesByParent[k] = v;
            }
            else
            {
                foreach (var t in v)
                {
                    TypesByParent[k].Add(t);
                }
            }
        }

        Linker.LinkAll(this);
    }

    protected bool Equals(Scope other)
    {
        var a = Types.Equals(other.Types);
        var b = Schemata.Equals(other.Schemata);
        // TODO the two below are very janky
        var c = Facts.Count.Equals(other.Facts.Count);
        var d = Instances.Count.Equals(other.Instances.Count);
        var e = Functions.Equals(other.Functions);

        return a && b && c && d && e;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Scope)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        Types.ToList().ForEach(t => hashCode.Add(t.Value));
        Schemata.ToList().ForEach(s => hashCode.Add(s.Value));
        Facts.ToList().ForEach(fs => fs.Value.ToList().ForEach(hashCode.Add));
        Instances.ToList().ForEach(i => hashCode.Add(i.Value));
        Functions.ToList().ForEach(f => hashCode.Add(f.Value));
        Actions.ToList().ForEach(a => hashCode.Add(a.Value));
        return hashCode.ToHashCode();
    }
}