using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Parenthetical : Expression
{
    public Expression Expression { get; }

    public Parenthetical(string text, Expression expression) : base(text)
    {
        Expression = expression;
    }

    public override object Evaluate(Context context)
    {
        return Expression.Evaluate(context);
    }

    public override IEnumerable<string> Terms()
    {
        return new HashSet<string>(Expression.Terms()) { Type.Name };
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Parenthetical other)
    {
        return Expression.Equals(other.Expression);
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Parenthetical)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return Expression.GetHashCode();
    }
}