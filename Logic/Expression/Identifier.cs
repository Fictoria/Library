using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;

namespace Fictoria.Logic.Expression;

public class Identifier : Expression
{
    public string Name { get; }

    public Identifier(string text, string name) : base(text)
    {
        Name = name;
    }
    
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

    protected bool Equals(Identifier other)
    {
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Identifier)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}