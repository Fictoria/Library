namespace Fictoria.Logic.Type;

public class Placeholder : Type
{
    public List<Placeholder> PlaceholderParents;
    
    public Placeholder(string name, List<Placeholder>? parents = null) : base(name, new List<Type>())
    {
        parents ??= new();
        PlaceholderParents = parents;
    }
}

public class Type
{  
    public static readonly Type Nothing   = new("nothing");
    public static readonly Type Anything  = new("anything");
    public static readonly Type Boolean   = new("bool");
    public static readonly Type Int       = new("int");
    public static readonly Type Float     = new("float");
    public static readonly Type Symbol    = new("symbol");
    public static readonly Type Object    = new("object");
    public static readonly Type Schema    = new("schema");
    public static readonly Type Function  = new("function");
    public static readonly HashSet<Type> BuiltIns = new()
    {
        Nothing, Anything, Boolean, Int, Float, Symbol, Object, Schema, Function
    };
    
    public string Name { get; }
    public IList<Type> Parents { get; set; }

    public Type(string name, List<Type>? parents = null)
    {
        Name = name;
        Parents = parents;
    }

    public bool IsA(Type other)
    {
        if (Name == other.Name) return true;
        if (Name != "nothing" && other.Name == "anything") return true;

        return Parents.Any(t => t.IsA(other));
    }

    public override string ToString()
    {
        return Name + (Parents.Count > 0 ? (": " + System.String.Join(", ", Parents.Select(t => t.Name))) : "");
    }

    protected bool Equals(Type other)
    {
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Type)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}