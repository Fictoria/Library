using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Double;

public class Abs : BuiltIn
{
    public string Name => "abs";
    public Type.Type Type => Logic.Type.Type.Int;

    public IList<Parameter> Parameters => new List<Parameter>
    {
        new("input", Logic.Type.Type.Float)
    };

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        if (arguments.Count != 1)
        {
            throw new EvaluateException($"built-in function `{Name}` takes exactly 1 argument");
        }

        var value = arguments[0].Evaluate(context);
        switch (value)
        {
            case long l:
                return Math.Abs(l);
            case double d:
                return Math.Abs(d);
            default:
                throw new EvaluateException($"first parameter ({value}) must be an int or float");
        }
    }
}