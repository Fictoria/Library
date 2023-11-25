using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Parenthetical : Expression
{
    public Expression Expression { get; }

    public Parenthetical(string text, Expression expression) : base(text)
    {
        Expression = expression;
    }

    public override object Evaluate(Context context)
    {
        return Expression.Evaluate(context);
    }
}