using Fictoria.Logic.Fact;
using Fictoria.Logic.Index;

namespace Fictoria.Logic.Evaluation;

public class Context
{
    public Stack<Scope> Stack { get; }

    public Context(Program program)
    {
        Stack = new Stack<Scope>();
        Stack.Push(program.Scope);
    }

    public void Push()
    {
        Stack.Push(new Scope());
    }

    public void Pop()
    {
        Stack.Pop();
    }

    public void Bind(string name, object value)
    {
        Stack.Peek().Bindings[name] = value;
    }

    // public void Unbind(string name)
    // {
    //     Stack.Peek().Bindings.Remove(name);
    // }

    public bool Resolve(string name, out object value)
    {
        foreach (var container in Stack)
        {
            if (!container.Bindings.TryGetValue(name, out var found))
            {
                continue;
            }

            value = found;
            return true;
        }

        value = null!;
        return false;
    }

    public bool ResolveType(string name, out Type.Type type)
    {
        foreach (var container in Stack)
        {
            if (!container.Types.TryGetValue(name, out var found))
            {
                continue;
            }

            type = found;
            return true;
        }

        type = null!;
        return false;
    }

    public bool ResolveSubtypes(Type.Type type, out List<Type.Type> subtypes)
    {
        var results = new List<Type.Type>();

        foreach (var container in Stack)
        {
            results.AddRange(container.GetAllSubtypes(type));
        }

        subtypes = results;
        return true;
    }

    public bool ResolveSchema(string name, out Schema schema)
    {
        foreach (var container in Stack)
        {
            if (!container.Schemata.TryGetValue(name, out var found))
            {
                continue;
            }

            schema = found;
            return true;
        }

        schema = null!;
        return false;
    }

    public bool ResolveSpatialIndex(string name, out SpatialIndex index)
    {
        foreach (var container in Stack)
        {
            if (!container.SpatialIndices.TryGetValue(name, out var found))
            {
                continue;
            }

            index = found;
            return true;
        }

        index = null!;
        return false;
    }

    public bool ResolveFact(string name, out ISet<Fact.Fact> facts)
    {
        foreach (var container in Stack)
        {
            if (!container.Facts.TryGetValue(name, out var found))
            {
                continue;
            }

            facts = found;
            return true;
        }

        facts = null!;
        return false;
    }

    public bool ResolveInstance(string name, out Instance instance)
    {
        foreach (var container in Stack)
        {
            if (!container.Instances.TryGetValue(name, out instance))
            {
                continue;
            }

            return true;
        }

        instance = null!;
        return false;
    }

    public bool ResolveInstances(Type.Type type, out List<Instance> instances)
    {
        var results = new List<Instance>();

        foreach (var container in Stack)
        {
            if (container.InstancesByType.TryGetValue(type.Name, out var found))
            {
                // TODO this is awful, why is InstancesByType only strings?
                results.AddRange(found.Select(i =>
                {
                    ResolveInstance(i, out var instance);
                    return instance;
                }));
            }
        }

        instances = results;
        return true;
    }

    public bool ResolveFunction(string name, out Function.Function function)
    {
        foreach (var scope in Stack)
        {
            if (!scope.Functions.TryGetValue(name, out var found))
            {
                continue;
            }

            function = found;
            return true;
        }

        function = null!;
        return false;
    }
}