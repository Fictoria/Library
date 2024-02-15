using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Double;

public class Floor : BuiltIn
{
    public string Name => "floor";
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

        var value = double.Parse(arguments[0].Evaluate(context).ToString()!);
        return Math.Floor(value);
    }
}