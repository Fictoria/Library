using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function;

public class Function
{
    public string Name { get; }
    public List<Parameter> Parameters { get; }
    public Expression.Expression Expression { get; }

    public Function(string name, List<Parameter> parameters, Expression.Expression expression)
    {
        Name = name;
        Parameters = parameters;
        Expression = expression;
    }

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        context.Push();
        for (var i = 0; i < arguments.Count; i++)
        {
            context.Bind(Parameters[i].Name, arguments[i]);
        }

        var result = Expression.Evaluate(context);
        context.Pop();
        return result;
    }

    public override string ToString()
    {
        return $"{Name}({String.Join(", ", Parameters.Select(p => p.ToString()))}) = {Expression}";
    }
}