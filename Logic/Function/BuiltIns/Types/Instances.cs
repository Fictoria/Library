using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Types;

public class Instances : BuiltIn
{
    public string Name => "instances";
    public Type.Type Type => Logic.Type.Type.Tuple;

    public IList<Parameter> Parameters => new List<Parameter>
    {
        new("type", Logic.Type.Type.Anything)
    };

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        if (arguments.Count != Parameters.Count)
        {
            throw new EvaluateException($"built-in function `{Name}` takes exactly {Parameters.Count} argument(s)");
        }

        var instances = new List<object>();
        var type = (Type.Type)arguments[0].Evaluate(context);
        if (context.ResolveInstances(type, out var found1))
        {
            instances.AddRange(found1);
        }

        if (context.ResolveSubtypes(type, out var subtypes))
        {
            foreach (var subtype in subtypes)
            {
                if (context.ResolveInstances(subtype, out var found2))
                {
                    instances.AddRange(found2);
                }
            }
        }

        return instances;
    }
}