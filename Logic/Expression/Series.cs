using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Series : Expression
{
    public Series(string text, IList<Expression> expressions) : base(text)
    {
        Expressions = expressions;
    }

    public IList<Expression> Expressions { get; }

    public override object Evaluate(Context context)
    {
        object result = false;

        foreach (var expression in Expressions)
        {
            result = expression.Evaluate(context);

            if (result is bool && (bool)result == false)
            {
                return false;
            }
        }

        return result;
    }

    public override IEnumerable<string> Terms()
    {
        return new HashSet<string>(Expressions.SelectMany(e => e.Terms())) { Type.Name };
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Series other)
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

        return Equals((Series)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        Expressions.ToList().ForEach(hashCode.Add);
        return hashCode.ToHashCode();
    }
}