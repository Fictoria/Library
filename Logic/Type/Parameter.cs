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

    public override string ToString()
    {
        return $"{Name}: {Type.Name}";
    }
}