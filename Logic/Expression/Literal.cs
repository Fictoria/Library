using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Literal : Expression
{
    public object Value { get; }

    public Literal(string text, object value, Type.Type type) : base(text, type)
    {
        Value = value;
    }
    
    public override object Evaluate(Context context)
    {
        return Value;
    }

    public override IEnumerable<string> Terms()
    {
        return new HashSet<string> { Type.Name };
    }

    protected bool Equals(Literal other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Literal)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}