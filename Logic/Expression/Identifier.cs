using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;

namespace Fictoria.Logic.Expression;

public class Identifier : Expression
{
    public Identifier(string text, string name) : base(text)
    {
        Name = name;
    }

    public string Name { get; }

    public override object Evaluate(Context context)
    {
        if (context.Resolve(Name, out var variable))
        {
            // if (variable.GetType().IsAssignableFrom(typeof(Expression))) 
            if (typeof(Expression).IsAssignableFrom(variable.GetType()))
            {
                return ((Expression)variable).Evaluate(context);
            }

            return variable;
        }

        if (context.ResolveInstance(Name, out var instance))
        {
            return instance;
        }

        if (context.ResolveType(Name, out var type))
        {
            return type;
        }

        throw new ResolveException($"unknown identifier '{Name}'");
    }

    public override IEnumerable<string> Terms()
    {
        return new HashSet<string> { Type.Name };
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Identifier other)
    {
        return Name == other.Name;
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

        return Equals((Identifier)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}