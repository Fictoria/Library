using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Double;

public class Arcsin : BuiltIn
{
    public string Name => "arcsin";
    public Type.Type Type => Logic.Type.Type.Float;

    public IList<Parameter> Parameters => new List<Parameter>
    {
        new("input", Logic.Type.Type.Float)
    };

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        if (arguments.Count != Parameters.Count)
        {
            throw new EvaluateException($"built-in function `{Name}` takes exactly {Parameters.Count} argument(s)");
        }

        var value = double.Parse(arguments[0].Evaluate(context).ToString()!);
        return Math.Asin(value);
    }
}