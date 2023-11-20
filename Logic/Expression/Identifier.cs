using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;

namespace Fictoria.Logic.Expression;

public class Identifier : Expression
{
    public Type.Type Type { get; set; }
    public string Name { get; }

    public Identifier(string name)
    {
        Name = name;
    }
    
    public object Evaluate(Context context)
    {
        if (context.Resolve(Name, out var variable))
        {
            return ((Expression)variable).Evaluate(context);
        }

        if (context.ResolveInstance(Name, out var instance))
        {
            return instance;
        }

        if (context.ResolveType(Name, out var type))
        {
            return type;
        }

        throw new ResolveException($"unknown symbol '{Name}'");
    }
}