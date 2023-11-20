using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Wildcard : Expression
{
    public Type.Type Type
    {
        get => Fictoria.Logic.Type.Type.Anything;
        set => throw new NotImplementedException();
    }

    public object Evaluate(Context context)
    {
        return this;
    }
}