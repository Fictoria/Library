using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Fact;

namespace Fictoria.Logic.Search;

public class InstanceSearch
{
    public static bool Search(Context context, Expression.Expression name, Expression.Expression type)
    {
        var nameIsBound = name is Binding;
        var typeIsBound = type is Binding;

        // instance(foo, bar)
        if (!nameIsBound && !typeIsBound)
        {
            var nameValue = (string)name.Evaluate(context);
            var typeValue = (Type.Type)type.Evaluate(context);
            if (context.ResolveInstance(nameValue, out var found))
            {
                // NOTE: invariant on purpose
                return found.Type.Equals(typeValue);
            }

            return false;
        }

        // instance(@foo, bar)
        if (nameIsBound && !typeIsBound)
        {
            var typeValue = (Type.Type)type.Evaluate(context);
            if (context.ResolveInstances(typeValue, out var found))
            {
                if (found.Count > 0)
                {
                    context.Bind(((Binding)name).Name, found[0]);
                    return true;
                }
            }

            return false;
        }

        // instance(foo, @bar)
        if (!nameIsBound && typeIsBound)
        {
            var nameValue = (string)name.Evaluate(context);
            if (context.ResolveInstance(nameValue, out var found))
            {
                context.Bind(nameValue, found.Type);
                return true;
            }
        }

        // instance(@foo, @bar)
        if (nameIsBound && typeIsBound)
        {
            throw new ParseException("the instance predicate does not support both arguments to be bound");
        }

        return false;
    }

    public static List<Instance> SearchAll(Context context, Expression.Expression name, Expression.Expression type)
    {
        var instances = new List<Instance>();
        var nameIsBound = name is Binding;
        var typeIsBound = type is Binding;

        // &instance(foo, bar)
        if (!nameIsBound && !typeIsBound)
        {
            throw new ParseException("match many found with no argument bindings");
        }

        // &instance(@foo, bar)
        if (nameIsBound && !typeIsBound)
        {
            var typeValue = (Type.Type)type.Evaluate(context);
            if (context.ResolveInstances(typeValue, out var found))
            {
                var names = found.Select(i => i.Name as object).ToList();
                context.Bind(((Binding)name).Name, names);
            }

            return instances;
        }

        // &instance(foo, @bar)
        if (!nameIsBound && typeIsBound)
        {
            // TODO not a great message – this doesn't really make sense for the invariant semantics of instance
            throw new ParseException("match many found with type binding");
        }

        // &instance(@foo, @bar)
        if (nameIsBound && typeIsBound)
        {
            throw new ParseException("the instance predicate does not support both arguments to be bound");
        }

        return instances;
    }
}