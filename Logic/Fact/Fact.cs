using System.Diagnostics.CodeAnalysis;

namespace Fictoria.Logic.Fact;

public class Fact
{
    public Schema Schema { get; set; }
    public List<Expression.Expression> Arguments { get; }

    public Fact(Schema schema, List<Expression.Expression> arguments)
    {
        Schema = schema;
        Arguments = arguments;
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        return Schema.Name + "(" + String.Join(", ", Arguments.Select(a => a.ToString())) + ")";
    }

    [ExcludeFromCodeCoverage]
    private bool Equals(Fact other)
    {
        return Schema.Equals(other.Schema) && Arguments.Equals(other.Arguments);
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Fact)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return HashCode.Combine(Schema, Arguments);
    }
}