namespace Fictoria.Planning.Utilities;

public class Pair<A, B>
{
    public A First { get; }
    public B Second { get; }

    public Pair(A first, B second)
    {
        First = first;
        Second = second;
    }

    private bool Equals(Pair<A, B> other)
    {
        return EqualityComparer<A>.Default.Equals(First, other.First) && EqualityComparer<B>.Default.Equals(Second, other.Second);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Pair<A, B>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(First, Second);
    }

    public void Deconstruct(out A first, out B second)
    {
        first = First;
        second = Second;
    }
}