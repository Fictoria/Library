using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;

namespace Fictoria.Logic.Expression;

public class Identifier : Expression
{
    public string Name { get; set; }

    public Identifier(string text, string name) : base(text)
    {
        Name = name;
    }

    public override object Evaluate(Context context)
    {
        if (context.Resolve(Name, out var variable))
        {
            if (variable is None)
            {
                return new None();
            }

            // TODO i don't remember what this was supposed to do, but it seems suspicious
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

        if (context.ResolveFunction(Name, out var func))
        {
            return func;
        }

        throw new ResolveException($"unknown identifier '{Name}'");
    }

    public override IEnumerable<string> Terms()
    {
        return new HashSet<string> { Type.Name };
    }

    public override Expression Clone()
    {
        return new Identifier(Text, Name);
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