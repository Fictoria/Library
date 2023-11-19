using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Literal : Expression
{
    public Type.Type Type { get; set; }
    public object Value { get; }

    public Literal(object value, Type.Type type)
    {
        Value = value;
        Type = type;
    }
    
    public object Evaluate(Context context)
    {
        return Value;
    }
}