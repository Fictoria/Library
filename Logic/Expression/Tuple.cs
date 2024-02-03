using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Tuple : Expression
{
    public Tuple(string text, IList<Expression> expressions) : base(text, Logic.Type.Type.Tuple)
    {
        Expressions = expressions;
    }

    public IList<Expression> Expressions { get; }

    public override object Evaluate(Context context)
    {
        return Expressions.Select(e => e.Evaluate(context)).ToList();
    }

    public override IEnumerable<string> Terms()
    {
        return new HashSet<string>(Expressions.SelectMany(e => e.Terms())) { Type.Name };
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Tuple other)
    {
        return Expressions.Equals(other.Expressions);
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Tuple)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        Expressions.ToList().ForEach(hashCode.Add);
        return hashCode.ToHashCode();
    }
}