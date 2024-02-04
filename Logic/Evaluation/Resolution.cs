using Fictoria.Logic.Expression;
using Fictoria.Logic.Fact;
using Tuple = Fictoria.Logic.Expression.Tuple;

namespace Fictoria.Logic.Evaluation;

public static class Resolution
{
    public static void Resolve(Context context, Expression.Expression expression)
    {
        switch (expression)
        {
            case Accessor accessor:
                Resolve(context, accessor.Structure);
                Resolve(context, accessor.Indexer);
                break;
            case Assign assign:
                Resolve(context, assign.Value);
                break;
            case Call call:
                foreach (var arg in call.Arguments)
                {
                    Resolve(context, arg);
                }

                break;
            case Identifier identifier:
                if (context.Resolve(identifier.Name, out var found))
                {
                    // TODO this definitely isn't complete either
                    if (found is string value)
                    {
                        identifier.Name = value;
                        identifier.Text = value;
                        break;
                    }

                    if (found is Instance instance)
                    {
                        identifier.Name = instance.Name;
                        identifier.Text = instance.Name;
                    }
                    // TODO throw something
                }

                break;
            case If @if:
                Resolve(context, @if.Condition);
                Resolve(context, @if.Body);
                if (@if.Else is not null)
                {
                    Resolve(context, @if.Else);
                }

                break;
            case Infix infix:
                Resolve(context, infix.Left);
                Resolve(context, infix.Right);
                break;
            case Parenthetical paren:
                Resolve(context, paren.Expression);
                break;
            case Series series:
                foreach (var exp in series.Expressions)
                {
                    Resolve(context, exp);
                }

                break;
            case Struct @struct:
                foreach (var (_, field) in @struct.Value)
                {
                    if (field.Expression is not null)
                    {
                        Resolve(context, field.Expression);
                    }
                }

                break;
            case Tuple tuple:
                foreach (var exp in tuple.Expressions)
                {
                    Resolve(context, exp);
                }

                break;
            case Unary unary:
                Resolve(context, unary.Expression);
                break;
        }
    }
}