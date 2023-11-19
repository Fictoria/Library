namespace Fictoria.Logic.Fact;

public class Instance
{
    public Type.Type Type { get; set; }
    public string Name { get; }

    public Instance(string name, Type.Type type)
    {
        Name = name;
        Type = type;
    }

    public override string ToString()
    {
        return Name;
    }

    protected bool Equals(Instance other)
    {
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Instance)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}