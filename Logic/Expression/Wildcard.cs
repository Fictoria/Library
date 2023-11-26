using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Wildcard : Expression
{
    public Wildcard() : base("_", Fictoria.Logic.Type.Type.Anything) {}

    public override object Evaluate(Context context)
    {
        return this;
    }

    protected bool Equals(Wildcard other)
    {
        return true;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Wildcard)obj);
    }

    public override int GetHashCode()
    {
        return "_".GetHashCode();
    }
}