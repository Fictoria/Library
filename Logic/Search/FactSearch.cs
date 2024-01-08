using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Fact;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Search;

public static class FactSearch
{
    public static bool Search(Context context, Schema schema, IEnumerable<Expression.Expression> args)
    {
        return FindFirst(context, schema, args, out _);
    }
    
    public static IList<FactResult> SearchAll(Context context, Schema schema, IEnumerable<Expression.Expression> args)
    {
        return FindAll(context, schema, args);
    }
    
    public static bool FindFirst(Context context, Schema schema, IEnumerable<Expression.Expression> args, out Fact.Fact outbound)
    {
        var arguments = args.ToList();
        if (arguments.Count != schema.Parameters.Count)
        {
            throw new EvaluateException($"invalid number of arguments passed to {schema.Name}");
        }

        var memoizer = new Memoizer(arguments);
        if (context.ResolveFact(schema.Name, out var facts))
        {
            foreach (var fact in facts)
            {
                if (matches(context, schema, fact, memoizer, arguments, out var bindings, out _))
                {
                    foreach (var (k, v) in bindings)
                    {
                        context.Bind(k, v);
                    }

                    outbound = fact;
                    return true;
                }
            }
        }

        outbound = null;
        return false;
    }
    
    //TODO turn this into an iterator, have FindFirst just take one
    public static IList<FactResult> FindAll(Context context, Schema schema, IEnumerable<Expression.Expression> args)
    {
        var results = new List<FactResult>();
        var seriesBindings = new List<(string, object)>();
        var arguments = args.ToList();
        if (arguments.Count != schema.Parameters.Count)
        {
            throw new EvaluateException($"invalid number of arguments passed to {schema.Name}");
        }

        var memoizer = new Memoizer(arguments);
        if (context.ResolveFact(schema.Name, out var facts))
        {
            foreach (var fact in facts)
            {
                if (matches(context, schema, fact, memoizer, arguments, out var bindings, out var values))
                {
                    foreach (var (k, v) in bindings)
                    {
                        seriesBindings.Add((k, v));
                        // context.Bind(k, v);
                    }

                    var result = new FactResult(fact.Schema.Name, values);
                    results.Add(result);
                }
            }
        }

        var groupedBindings =
            seriesBindings
                .GroupBy(b => b.Item1)
                .Select(g => 
                    (g.Key, g.Select(b => b.Item2).ToList())
                );
        foreach (var (variable, tuple) in groupedBindings)
        {
            context.Bind(variable, tuple);
        }

        return results;
    }
    
    public static IList<Fact.Fact> FindAllRaw(Context context, Schema schema, IEnumerable<Expression.Expression> args)
    {
        var results = new List<Fact.Fact>();
        var arguments = args.ToList();
        if (arguments.Count != schema.Parameters.Count)
        {
            throw new EvaluateException($"invalid number of arguments passed to {schema.Name}");
        }

        var memoizer = new Memoizer(arguments);
        if (context.ResolveFact(schema.Name, out var facts))
        {
            foreach (var fact in facts)
            {
                if (matches(context, schema, fact, memoizer, arguments, out var bindings, out var values))
                {
                    foreach (var (k, v) in bindings)
                    {
                        context.Bind(k, v);
                    }

                    results.Add(fact);
                }
            }
        }

        return results;
    }

    private static bool matches(
        Context context,
        Schema schema,
        Fact.Fact fact,
        Memoizer memoizer,
        IList<Expression.Expression> arguments,
        out IDictionary<string, object> bindings,
        out IList<object> values)
    {
        var found = false;
        var bindingsResult = new Dictionary<string, object>();
        var valuesResult = new List<object>();
        for (int i = 0; i < fact.Arguments.Count; i++)
        {
            var value = fact.Arguments[i].Evaluate(context);
            valuesResult.Add(value);
            context.Bind("$", value);
            if (memoizer.Value(context, i) is Wildcard)
            {
                found = true;
            }
            else if (arguments[i] is Binding b)
            {
                found = true;
                bindingsResult[b.Name] = value;
            }
            else if (value.Equals(memoizer.Value(context, i)) && !(arguments[i].ContainsBinding && value is false))
            {
                found = true;
            }
            else if (arguments[i].ContainsBinding && memoizer.Value(context, i) is true)
            {
                found = true;
                bindingsResult[arguments[i].BindingName] = value;
            }
            else if (schema.Parameters[i].Variance == Variance.Covariant && value is Type.Type t1 && ((memoizer.Value(context, i) is Instance i1 && i1.Type.IsA(t1)) || (memoizer.Value(context, i) is Type.Type x1 && x1.IsA(t1))))
            {
                found = true;
            }
            else if (schema.Parameters[i].Variance == Variance.Contravariant &&memoizer.Value(context, i) is Type.Type t2 && ((value is Instance i2 && i2.Type.IsA(t2)) || (value is Type.Type x2 && x2.IsA(t2))))
            {
                found = true;
            }
            else
            {
                found = false;
            }

            if (!found) break;
        }

        bindings = bindingsResult;
        values = valuesResult;
        return found;
    }
}