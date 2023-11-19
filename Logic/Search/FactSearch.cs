using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Fact;

namespace Fictoria.Logic.Search;

public class FactSearch
{
    public static bool Search(Context context, Schema schema, List<Expression.Expression> arguments)
    {
        if (arguments.Count != schema.Parameters.Count)
        {
            throw new EvaluateException($"invalid number of arguments passed to {schema.Name}");
        }

        var values = arguments.Select(a => a.Evaluate(context)).ToList();
        if (context.ResolveFact(schema.Name, out var facts))
        {
            foreach (var fact in facts)
            {
                var found = false;
                for (int i = 0; i < fact.Arguments.Count; i++)
                {
                    var value = fact.Arguments[i].Evaluate(context);
                    if (value.Equals(values[i]))
                    {
                        found = true;
                    }
                    else if (fact.Arguments[i].GetType() == typeof(Type.Type) &&
                             ((Instance)value).Type.IsA((Type.Type)value))
                    {
                        found = true;
                    }

                    if (!found) break;
                }
                
                if (found) return true;
            }
        }
        
        return false;
    }
}