using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Literal : Expression
{
    public Literal(string text, object value, Type.Type type) : base(text, type)
    {
        Value = value;
    }

    public object Value { get; }

    public override object Evaluate(Context context)
    {
        return Value;
    }

    public override IEnumerable<string> Terms()
    {
        return new HashSet<string> { Type.Name };
    }

    public override Expression Clone()
    {
        return new Literal(Text, Value, Type);
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Literal other)
    {
        return Value.Equals(other.Value);
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

        return Equals((Literal)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}