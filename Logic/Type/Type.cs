namespace Fictoria.Logic.Type;

public class TypePlaceholder : Type
{
    public List<TypePlaceholder> PlaceholderParents;
    
    public TypePlaceholder(string name, List<TypePlaceholder>? parents = null) : base(name, new List<Type>())
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
    public static readonly Type String    = new("string");
    public static readonly Type Symbol    = new("symbol");
    public static readonly Type Object    = new("object");
    public static readonly Type Struct    = new("struct");
    public static readonly Type Schema    = new("schema");
    public static readonly Type Function  = new("function");
    public static readonly Type Variable  = new("variable");
    public static readonly Type Tuple     = new("tuple");
    public static readonly HashSet<Type> BuiltIns = new()
    {
        Nothing, Anything, Boolean, Int, Float, String, Symbol, Object, Struct, Schema, Function
    };
    
    public string Name { get; }
    public IList<Type> Parents { get; set; }

    public Type(string name, List<Type>? parents = null)
    {
        Name = name;
        Parents = parents ?? new List<Type>();
    }

    public bool IsA(Type other)
    {
        // ReSharper disable once PossibleUnintendedReferenceComparison
        if (this == Anything) return true;
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