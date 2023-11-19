using Fictoria.Logic.Type;

namespace Fictoria.Logic.Fact;

public class Schema
{
    public string Name { get; }
    public List<Parameter> Parameters { get; }

    public Schema(string name, List<Parameter> parameters)
    {
        Name = name;
        Parameters = parameters;
    }

    public override string ToString()
    {
        return Name + "(" + String.Join(", ", Parameters.Select(p => p.Name + ": " + p.Type.Name)) + ")";
    }

    protected bool Equals(Schema other)
    {
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Schema)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}