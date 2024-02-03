using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Function;
using Fictoria.Logic.Search;

namespace Fictoria.Logic.Expression;

public class Call : Expression
{
    public Call(string text, string functor, IList<Expression> arguments, bool many = false) : base(text)
    {
        Functor = functor;
        Arguments = arguments;
        Many = many;
    }

    public bool Many { get; }
    public string Functor { get; }
    public IList<Expression> Arguments { get; }

    public override object Evaluate(Context context)
    {
        if (Functor == "instance")
        {
            // TODO do search and many search just like facts
        }

        if (context.ResolveSchema(Functor, out var schema))
        {
            if (Many)
            {
                return FactSearch.SearchAll(context, schema, Arguments);
            }

            return FactSearch.Search(context, schema, Arguments);
        }

        if (context.ResolveFunction(Functor, out var function))
        {
            return function.Evaluate(context, Arguments);
        }

        if (Builtins.ByName.TryGetValue(Functor, out var builtin))
        {
            return builtin.Evaluate(context, Arguments);
        }

        throw new EvaluateException($"call expression with functor '{Functor}' is not a schema or function");
    }

    public override IEnumerable<string> Terms()
    {
        return new HashSet<string>(Arguments.SelectMany(a => a.Terms())) { Functor, Type.Name };
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Call other)
    {
        return Functor == other.Functor && Arguments.Equals(other.Arguments);
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

        return Equals((Call)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return HashCode.Combine(Functor, Arguments);
    }
}