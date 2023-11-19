using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Search;

namespace Fictoria.Logic.Expression;

public class Call : Expression
{
    public Type.Type Type { get; set; }
    public string Functor { get; }
    public List<Expression> Arguments { get; }

    public Call(string functor, List<Expression> arguments)
    {
        Functor = functor;
        Arguments = arguments;
    }

    public object Evaluate(Context context)
    {
        if (context.ResolveSchema(Functor, out var schema))
        {
            return FactSearch.Search(context, schema, Arguments);
        }

        if (context.ResolveFunction(Functor, out var function))
        {
            return function.Evaluate(context, Arguments);
        }

        throw new EvaluateException($"call expression with functor '{Functor}' is not a schema or function");
    }

    public override string ToString()
    {
        return $"{Functor}({String.Join(", ", Arguments.Select(a => a.ToString()))})";
    }
}