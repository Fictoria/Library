namespace Fictoria.Logic.Expression;

public class Search : Infix
{
    public Binding Binding { get; }
    
    public Search(string text, Binding left, string op, Expression right)
        : base(text, new Identifier("$", "$"), op, right)
    {
        Binding = left;
    }

    protected bool Equals(Search other)
    {
        return base.Equals(other) && Binding.Equals(other.Binding);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Search)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Binding);
    }
}