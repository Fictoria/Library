using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns;

public interface BuiltIn
{
    public string Name { get; }
    public Type.Type Type { get; }
    public IList<Parameter> Parameters { get; }
    public object Evaluate(Context context, IList<Expression.Expression> arguments);
}