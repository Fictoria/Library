using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Fact;

public class SchemaPlaceholder : Schema
{
    public SchemaPlaceholder(string name) : base(name, new List<Parameter>()) {}
}

public class Schema
{
    public string Name { get; }
    public List<Parameter> Parameters { get; }

    public Schema(string name, List<Parameter> parameters)
    {
        Name = name;
        Parameters = parameters;
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        return Name + "(" + String.Join(", ", Parameters.Select(p => p.Name + ": " + p.Type.Name)) + ")";
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Schema other)
    {
        return Name == other.Name;
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Schema)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}