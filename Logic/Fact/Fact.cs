using System.Diagnostics.CodeAnalysis;

namespace Fictoria.Logic.Fact;

public class Fact
{
    public Fact(Schema schema, List<Expression.Expression> arguments)
    {
        Schema = schema;
        Arguments = arguments;
    }

    public Fact(Fact fact)
    {
        Schema = fact.Schema;
        Arguments = fact.Arguments.Select(a => a.Clone()).ToList();
    }

    public Schema Schema { get; set; }
    public List<Expression.Expression> Arguments { get; }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        return Schema.Name + "(" + string.Join(", ", Arguments.Select(a => a.ToString())) + ")";
    }

    [ExcludeFromCodeCoverage]
    private bool Equals(Fact other)
    {
        return Schema.Equals(other.Schema) && Arguments.Equals(other.Arguments);
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

        return Equals((Fact)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return HashCode.Combine(Schema, Arguments);
    }
}