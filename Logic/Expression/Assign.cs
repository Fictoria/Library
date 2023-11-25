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
}