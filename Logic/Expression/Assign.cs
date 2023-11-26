using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Assign : Expression
{
    public string Variable { get; }
    public Expression Value { get; }

    public Assign(string text, string variable, Expression value) : base(text)
    {
        Variable = variable;
        Value = value;
    }

    public override object Evaluate(Context context)
    {
        var result = Value.Evaluate(context);
        context.Bind(Variable, result);
        return result;
    }

    protected bool Equals(Assign other)
    {
        return Variable == other.Variable && Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Assign)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Variable, Value);
    }
}