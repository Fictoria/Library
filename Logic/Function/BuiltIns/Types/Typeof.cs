using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Fact;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Types;

public class Typeof : BuiltIn
{
    public string Name => "typeof";
    public Type.Type Type => Logic.Type.Type.Anything;

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

        var input = arguments[0].Evaluate(context);
        switch (input)
        {
            case Instance instance:
                return instance.Type;
            case Logic.Type.Type type:
                return type;
        }
        throw new EvaluateException($"unknown input `{input}`");
    }
}