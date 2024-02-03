using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Expression;

namespace Fictoria.Logic.Fact;

public class Antifact
{
    public Antifact(Call matcher)
    {
        Matcher = matcher;
    }

    public Call Matcher { get; set; }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        return $"~{Matcher}";
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Antifact other)
    {
        return Matcher.Equals(other.Matcher);
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Antifact)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return Matcher.GetHashCode();
    }
}