using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Types;

public class Subtypes : BuiltIn
{
    public string Name => "subtypes";
    public Type.Type Type => Logic.Type.Type.Tuple;

    public IList<Parameter> Parameters => new List<Parameter>
    {
        new("input", Logic.Type.Type.Anything)
    };

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        if (arguments.Count != Parameters.Count)
        {
            throw new EvaluateException($"built-in function `{Name}` takes exactly {Parameters.Count} argument(s)");
        }

        var value = arguments[0].Evaluate(context);
        if (value is Type.Type type && context.ResolveSubtypes(type, out var subtypes))
        {
            return subtypes.Select(st => st as object).ToList();
        }

        throw new EvaluateException($"argument passed to {Name} was not a type");
    }
}