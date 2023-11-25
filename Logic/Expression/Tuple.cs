using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Tuple : Expression
{
    public IList<Expression> Expressions { get; }

    public Tuple(string text, IList<Expression> expressions) : base(text)
    {
        Expressions = expressions;
    }

    public override object Evaluate(Context context)
    {
        return Expressions.Select(e => e.Evaluate(context)).ToList();
    }
}