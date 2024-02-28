using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Struct : Expression
{
    public IDictionary<string, Field> Value { get; }

    public Struct(string text, IEnumerable<(string, Field)> fields) : base(text, Logic.Type.Type.Struct)
    {
        Value = new Dictionary<string, Field>();
        foreach (var (key, value) in fields)
        {
            Value[key] = value;
        }
    }

    public Field? GetOrNull(string key)
    {
        return Value.TryGetValue(key, out var field) ? field : null;
    }

    public override object Evaluate(Context context)
    {
        var result = new Dictionary<string, object>();
        foreach (var (key, value) in Value)
        {
            if (value.Expression != null)
            {
                var temp = value.Expression.Evaluate(context);
                result[key] = temp;
            }
            else
            {
                result[key] = value.Statements!;
            }
        }

        return result;
    }

    public override IEnumerable<string> Terms()
    {
        return Array.Empty<string>();
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Struct other)
    {
        return Value.Equals(other.Value);
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

        return Equals((Struct)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}