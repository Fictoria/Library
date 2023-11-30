using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Types;

public class Str : BuiltIn
{
    public string Name => "str";
    public Type.Type Type => Logic.Type.Type.String;

    public IList<Parameter> Parameters => new List<Parameter>
    {
        new("input", Logic.Type.Type.Anything)
    };
    
    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        if (arguments.Count != 1) throw new EvaluateException($"built-in function `{Name}` takes exactly 1 argument");

        var value = arguments[0].Evaluate(context);
        return value.ToString() ?? "null";
    }
}