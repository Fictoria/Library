using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Fact;

namespace Fictoria.Logic.Expression;

public class Infix : Expression
{
    public Expression Left { get; }
    public Expression Right { get; }
    public string Operator { get; }

    public Infix(string text, Expression left, string op, Expression right) : base(text)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
    
    // TODO this method is long
    public override object Evaluate(Context context)
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
                case "^":
                    return Math.Pow(left, right);
            }
        }
        else if (Type.Equals(Fictoria.Logic.Type.Type.Float))
        {
            // TODO this is inelegant and slow
            var left = double.Parse(Left.Evaluate(context).ToString());
            var right = double.Parse(Right.Evaluate(context).ToString());

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
                case "^":
                    throw new EvaluateException($"exponents are not valid for floats");
            }
        }
        else if (Type.Equals(Fictoria.Logic.Type.Type.Boolean))
        {
            if (Operator == "::")
            {
                var left = Left.Evaluate(context);
                var right = (Type.Type)Right.Evaluate(context);
                //TODO no guarantee this is an instance, need a runtime type checker
                return ((Instance)left).Type.IsA(right);
            }
            
            if (Left.Type.Equals(Fictoria.Logic.Type.Type.Int))
            {
                var left = (long)Left.Evaluate(context);
                var right = (long)Right.Evaluate(context);
                
                switch (Operator)
                {
                    case ">":
                        return left > right;
                    case "<":
                        return left < right;
                    case ">=":
                        return left >= right;
                    case "<=":
                        return left <= right;
                    case "==":
                        return left == right;
                    case "!=":
                        return left != right;
                }
            }
            else if (Left.Type.Equals(Fictoria.Logic.Type.Type.Float))
            {
                var left = (double)Left.Evaluate(context);
                var right = (double)Right.Evaluate(context);
                
                switch (Operator)
                {
                    case ">":
                        return left > right;
                    case "<":
                        return left < right;
                    case ">=":
                        return left >= right;
                    case "<=":
                        return left <= right;
                    case "==":
                        // TODO epsilon
                        return left == right;
                    case "!=":
                        // TODO epsilon
                        return left != right;
                }
            }
            else if (Left.Type.Equals(Fictoria.Logic.Type.Type.Boolean))
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
        }

        throw new EvaluateException($"invalid type '{Type}' for '{Operator}' infix expression");
    }

    public override IEnumerable<string> Terms()
    {
        var terms = new HashSet<string>(Left.Terms()) { Type.Name };
        Right.Terms().ToList().ForEach(t => terms.Add(t));
        return terms;
    }

    protected bool Equals(Infix other)
    {
        return Left.Equals(other.Left) && Right.Equals(other.Right) && Operator == other.Operator;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Infix)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Left, Right, Operator);
    }
}