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
        throw new NotImplementedException();
    }

    public override IEnumerable<string> Terms()
    {
        throw new NotImplementedException();
    }
}