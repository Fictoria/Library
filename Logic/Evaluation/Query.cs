namespace Fictoria.Logic.Evaluation;

public class Query
{
    public Query(Expression.Expression expression)
    {
        Expression = expression;
    }

    public Expression.Expression Expression { get; }
}