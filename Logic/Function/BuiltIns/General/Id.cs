using Fictoria.Domain.Utilities;
using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.General;

public class Id : BuiltIn
{
    public string Name => "id";
    public Type.Type Type => Logic.Type.Type.String;

    public IList<Parameter> Parameters => new List<Parameter>();

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        if (arguments.Count != Parameters.Count)
        {
            throw new EvaluateException($"built-in function `{Name}` takes exactly {Parameters.Count} parameters");
        }

        return Identifier.Random();
    }
}