using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Double;

public class Clamp : BuiltIn
{
    public string Name => "clamp";
    public Type.Type Type => Logic.Type.Type.Int;

    public IList<Parameter> Parameters => new List<Parameter>
    {
        new("value", Logic.Type.Type.Float),
        new("min", Logic.Type.Type.Float),
        new("max", Logic.Type.Type.Float)
    };

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        if (arguments.Count != Parameters.Count)
        {
            throw new EvaluateException($"built-in function `{Name}` takes exactly {Parameters.Count} argument(s)");
        }
        
        var value = arguments[0].Evaluate(context);
        var min = arguments[1].Evaluate(context);
        var max = arguments[2].Evaluate(context);
        
        switch (value, min, max)
        {
            case (long lval, long lmin, long lmax):
                return Math.Clamp(lval, lmin, lmax);
            case (double dval, double dmin, double dmax):
                return Math.Clamp(dval, dmin, dmax);
            default:
                throw new EvaluateException($"first parameter ({value}) must be an int or float");
        }
    }
}