using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public interface Expression
{
    public Type.Type Type { get; set; }
    public object Evaluate(Context context);
}