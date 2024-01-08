using System.Diagnostics.CodeAnalysis;

namespace Fictoria.Logic.Type;

public class Parameter
{
    public string Name { get; }
    public Type Type { get; set; }
    public Variance Variance { get; }

    public Parameter(string name, Type type, Variance variance = Variance.Invariant)
    {
        Name = name;
        Type = type;
        Variance = variance;
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        return $"{Name}: {Type.Name}";
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Parameter other)
    {
        return Name == other.Name && Type.Equals(other.Type) && Variance == other.Variance;
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Parameter)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Type, (int)Variance);
    }
}