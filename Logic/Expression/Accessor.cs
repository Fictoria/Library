using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;

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
        var dict = (Dictionary<string, object>)Structure.Evaluate(context);
        var index = (string)Indexer.Evaluate(context);

        return dict[index];
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
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Accessor)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return HashCode.Combine(Structure, Indexer);
    }
}