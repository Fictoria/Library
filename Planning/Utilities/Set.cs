namespace Fictoria.Planning.Utilities;

public static class SetExtensions
{
    public static IEnumerable<Pair<A, B>> CartesianProduct<A, B>(this IEnumerable<A> self, IEnumerable<B> other)
    {
        return from a in self from b in other select new Pair<A, B>(a, b);
    }
}