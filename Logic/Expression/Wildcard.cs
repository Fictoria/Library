using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Wildcard : Expression
{
    public Wildcard() : base("_", Fictoria.Logic.Type.Type.Anything) {}

    public override object Evaluate(Context context)
    {
        return this;
    }
}