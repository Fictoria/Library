using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Parenthetical : Expression
{
    public Expression Expression { get; }

    public Type.Type Type { get; set; }

    public Parenthetical(Expression expression)
    {
        Expression = expression;
        Type = Fictoria.Logic.Type.Type.Nothing;
    }

    public object Evaluate(Context context)
    {
        return Expression.Evaluate(context);
    }

    public override string ToString()
    {
        return $"({Expression})";
    }
}