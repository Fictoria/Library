namespace Fictoria.Logic.Index;

public class Using
{
    public Expression.Expression X { get; }
    public Expression.Expression Y { get; }
    public Expression.Expression Distance { get; }

    public Using(Expression.Expression x, Expression.Expression y, Expression.Expression distance)
    {
        X = x;
        Y = y;
        Distance = distance;
    }
}