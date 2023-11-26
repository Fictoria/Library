using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Tuple : Expression
{
    public IList<Expression> Expressions { get; }

    public Tuple(string text, IList<Expression> expressions) : base(text)
    {
        Expressions = expressions;
    }

    public override object Evaluate(Context context)
    {
        return Expressions.Select(e => e.Evaluate(context)).ToList();
    }

    protected bool Equals(Tuple other)
    {
        return Expressions.Equals(other.Expressions);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Tuple)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        Expressions.ToList().ForEach(hashCode.Add);
        return hashCode.ToHashCode();
    }
}