using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;

namespace Fictoria.Logic.Expression;

public class Accessor : Expression
{
    public Expression Structure { get; }
    public Expression Indexer { get; }

    public Accessor(string text, Expression structure, Expression indexer) : base(text, Logic.Type.Type.Anything)
    {
        Structure = structure;
        Indexer = indexer;
    }

    public override object Evaluate(Context context)
    {
        var target = Structure.Evaluate(context);
        var index = Indexer.Evaluate(context);

        switch (target)
        {
            case Dictionary<string, object> dict:
                return dict[(string)index];
            case List<object> list:
                var asInt = Convert.ToInt32(index);
                if (asInt >= list.Count)
                {
                    return new None();
                }
                return list[asInt];
            case None none:
                return none;
        }

        throw new EvaluateException($"`{Structure.Text}` is not a struct or tuple");
    }

    public override IEnumerable<string> Terms()
    {
        return Array.Empty<string>();
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Accessor other)
    {
        return Structure.Equals(other.Structure) && Indexer.Equals(other.Indexer);
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

        return Equals((Accessor)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return HashCode.Combine(Structure, Indexer);
    }
}