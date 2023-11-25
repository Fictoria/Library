using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;

namespace Fictoria.Logic.Expression;

public class Unary : Expression
{
    public string Operator { get; }
    public Expression Expression { get; }

    public Unary(string text, string op, Expression expression) : base(text)
    {
        Operator = op;
        Expression = expression;
    }
    
    public override object Evaluate(Context context)
    {
        var result = Expression.Evaluate(context);
        
        // TODO this assumes one operator per type
        if (Type.Equals(Fictoria.Logic.Type.Type.Int))
        {
            return (long)result * -1;
        }
        if (Type.Equals(Fictoria.Logic.Type.Type.Float))
        {
            return (double)result * -1.0;
        }
        if (Type.Equals(Fictoria.Logic.Type.Type.Boolean))
        {
            return !(bool)result;
        }
        
        throw new EvaluateException($"invalid type '{Type}' for '{Operator}' unary expression");
    }
}