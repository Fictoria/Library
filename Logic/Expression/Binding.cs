using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;

namespace Fictoria.Logic.Expression;

public class Binding : Expression
{
    public string Name { get; }

    public Binding(string text, string name) : base(text)
    {
        Name = name;
        ContainsBinding = true;
        BindingName = name;
    }

    public override object Evaluate(Context context)
    {
        if (context.Resolve("$", out var found))
        {
            return found;
        }

        throw new EvaluateException($"conditional search missing binding");
    }

    public override IEnumerable<string> Terms()
    {
        return new HashSet<string> { Type.Name };
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Binding other)
    {
        return Name == other.Name;
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Binding)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}