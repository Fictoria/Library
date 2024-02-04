using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Fact;

public class Instance
{
    public Instance(Expression.Expression nameExpression, Expression.Expression typeExpression, string name = "",
        Type.Type? type = null)
    {
        Name = name;
        NameExpression = nameExpression;
        Type = type ?? Logic.Type.Type.Nothing;
        TypeExpression = typeExpression;
    }

    public Instance(Instance instance)
    {
        Name = instance.Name;
        NameExpression = instance.NameExpression;
        Type = instance.Type;
        TypeExpression = instance.TypeExpression;
    }

    public string Name { get; set; }
    public Expression.Expression NameExpression { get; }
    public Type.Type Type { get; set; }
    public Expression.Expression TypeExpression { get; }

    public void Resolve(Context context)
    {
        Name = (string)NameExpression.Evaluate(context);
        Type = (Type.Type)TypeExpression.Evaluate(context);
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        return Name;
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Instance other)
    {
        return Name == other.Name;
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

        return Equals((Instance)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}