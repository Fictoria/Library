using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Struct : Expression
{
    public IDictionary<string, Field> Value { get; }
    
    public Struct(string text, IEnumerable<(string, Field)> fields) : base(text, Logic.Type.Type.Struct)
    {
        Value = new Dictionary<string, Field>();
        foreach (var (key, value) in fields)
        {
            Value[key] = value;
        }
    }

    public override object Evaluate(Context context)
    {
        var result = new Dictionary<string, object>();
        foreach (var (key, value) in Value)
        {
            if (value.Expression != null)
            {
                var temp = value.Expression.Evaluate(context);
                result[key] = temp;
            }
            else
            {
                result[key] = value.Statements!;
            }
        }
        return result;
    }

    public override IEnumerable<string> Terms()
    {
        return Array.Empty<string>();
    }
}