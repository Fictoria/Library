using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Search;

namespace Fictoria.Logic.Expression;

public class Call : Expression
{
    public string Functor { get; }
    public IEnumerable<Expression> Arguments { get; }

    public Call(string text, string functor, IEnumerable<Expression> arguments) : base(text)
    {
        Functor = functor;
        Arguments = arguments;
    }

    public override object Evaluate(Context context)
    {
        if (context.ResolveSchema(Functor, out var schema))
        {
            return FactSearch.Search(context, schema, Arguments);
        }

        if (context.ResolveFunction(Functor, out var function))
        {
            return function.Evaluate(context, Arguments.ToList());
        }

        throw new EvaluateException($"call expression with functor '{Functor}' is not a schema or function");
    }
}