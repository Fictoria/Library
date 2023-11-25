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
}