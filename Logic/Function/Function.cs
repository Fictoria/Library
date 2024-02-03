using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function;

public class Function
{
    public Function(string name, List<Parameter> parameters, Expression.Expression expression)
    {
        Name = name;
        Parameters = parameters;
        Expression = expression;
    }

    public string Name { get; }
    public List<Parameter> Parameters { get; }
    public Expression.Expression Expression { get; }

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        context.Push();
        for (var i = 0; i < arguments.Count; i++)
        {
            var value = arguments[i].Evaluate(context);
            context.Bind(Parameters[i].Name, value);
        }

        var result = Expression.Evaluate(context);
        context.Pop();
        return result;
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        return $"{Name}({string.Join(", ", Parameters.Select(p => p.ToString()))}) = {Expression}";
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Function other)
    {
        return Name == other.Name && Parameters.Equals(other.Parameters) && Expression.Equals(other.Expression);
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Function)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        Parameters.ToList().ForEach(hashCode.Add);
        return HashCode.Combine(Name, hashCode.ToHashCode(), Expression);
    }
}