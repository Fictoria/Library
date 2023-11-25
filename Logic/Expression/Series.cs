using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Series : Expression
{
    public IList<Expression> Expressions { get; }

    public Series(string text, IList<Expression> expressions) : base(text)
    {
        Expressions = expressions;
    }

    public override object Evaluate(Context context)
    {
        object result = false;
        
        foreach (var expression in Expressions)
        {
            result = expression.Evaluate(context);

            if (result is bool && (bool)result == false)
            {
                return false;
            }
        }

        return result;
    }
}