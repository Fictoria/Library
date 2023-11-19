namespace Fictoria.Logic.Type;

public class Parameter
{
    public string Name { get; }
    public Type Type { get; set; }

    public Parameter(string name, Type type)
    {
        Name = name;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Name}: {Type.Name}";
    }
}