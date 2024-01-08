using System.Diagnostics.CodeAnalysis;

namespace Fictoria.Logic.Fact;

public class Instance
{
    public string Name { get; set; }
    public Expression.Expression NameExpression { get; }
    public Type.Type Type { get; set; }
    public Expression.Expression TypeExpression { get; }

    public Instance(Expression.Expression nameExpression, Expression.Expression typeExpression, string name = "", Type.Type? type = null)
    {
        Name = name;
        NameExpression = nameExpression;
        Type = type ?? Fictoria.Logic.Type.Type.Nothing;
        TypeExpression = typeExpression;
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        return Name.ToString();
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Instance other)
    {
        return Name == other.Name;
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Instance)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}