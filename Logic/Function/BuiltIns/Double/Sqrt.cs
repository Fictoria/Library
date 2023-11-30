using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Double;

public class Sqrt : BuiltIn
{
    public string Name => "sqrt";
    public Type.Type Type => Logic.Type.Type.Float;

    public IList<Parameter> Parameters => new List<Parameter>
    {
        new("input", Logic.Type.Type.Float)
    };
    
    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        if (arguments.Count != 1) throw new EvaluateException($"built-in function `{Name}` takes exactly 1 argument");

        var value = (double)arguments[0].Evaluate(context);
        return Math.Sqrt(value);
    }
}