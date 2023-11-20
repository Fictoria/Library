using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;

namespace Fictoria.Logic.Expression;

public class Infix : Expression
{
    public Type.Type Type { get; set; }
    public Expression Left { get; }
    public Expression Right { get; }
    public string Operator { get; }

    public Infix(Expression left, string op, Expression right)
    {
        Left = left;
        Operator = op;
        Right = right;
        Type = Fictoria.Logic.Type.Type.Nothing;
    }
    
    public object Evaluate(Context context)
    {
        if (Type.Equals(Fictoria.Logic.Type.Type.Int))
        {
            var leftRaw = Left.Evaluate(context);
            var rightRaw = Right.Evaluate(context);
            
            // TODO this is inelegant and slow
            var left = long.Parse(leftRaw.ToString());
            var right = long.Parse(rightRaw.ToString());

            switch (Operator)
            {
                case "+":
                    return left + right;
                case "-":
                    return left - right;
                case "*":
                    return left * right;
                case "/":
                    // TODO hmm
                    return left / right;
            }
        }
        else if (Type.Equals(Fictoria.Logic.Type.Type.Float))
        {
            var left = (double)Left.Evaluate(context);
            var right = (double)Right.Evaluate(context);

            switch (Operator)
            {
                case "+":
                    return left + right;
                case "-":
                    return left - right;
                case "*":
                    return left * right;
                case "/":
                    return left / right;
            }
        }
        else if (Type.Equals(Fictoria.Logic.Type.Type.Boolean))
        {
            var left = (bool)Left.Evaluate(context);
            var right = (bool)Right.Evaluate(context);

            switch (Operator)
            {
                case "and":
                    return left && right;
                case "or":
                    return left || right;
                case "xor":
                    return left ^ right;
            }
        }

        throw new EvaluateException($"invalid type '{Type}' for '{Operator}' infix expression");
    }
}