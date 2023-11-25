using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Fact;

namespace Fictoria.Logic.Search;

public static class FactSearch
{
    public static bool Search(Context context, Schema schema, IEnumerable<Expression.Expression> args)
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
                var found = false;
                var bindings = new Dictionary<string, object>();
                for (int i = 0; i < fact.Arguments.Count; i++)
                {
                    var value = fact.Arguments[i].Evaluate(context);
                    context.Bind("$", value);
                    if (memoizer.Value(context, i) is Wildcard)
                    {
                        found = true;
                    }
                    else if (value.Equals(memoizer.Value(context, i)) && !(arguments[i].ContainsBinding && value is false))
                    {
                        found = true;
                    }
                    else if (arguments[i].ContainsBinding && memoizer.Value(context, i) is true)
                    {
                        found = true;
                        bindings[arguments[i].BindingName] = value;
                    }
                    else if (value is Type.Type t1 && memoizer.Value(context, i) is Instance i1 && i1.Type.IsA(t1))
                    {
                        found = true;
                    }
                    else if (memoizer.Value(context, i) is Type.Type t2 && value is Instance i2 && i2.Type.IsA(t2))
                    {
                        found = true;
                    }
                    else
                    {
                        found = false;
                    }
                    context.Unbind("$");

                    if (!found) break;
                }

                if (found)
                {
                    foreach (var (k, v) in bindings)
                    {
                        context.Bind(k, v);
                    }
                    return true;
                }
            }
        }
        
        return false;
    }
}