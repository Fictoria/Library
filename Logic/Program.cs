using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Search;

namespace Fictoria.Logic;

public class Program
{
    public Scope Scope { get; }

    public Program(Scope scope)
    {
        Scope = scope;
    }

    public object Evaluate(string code)
    {
        var context = new Context(this);
        var expression = Loader.LoadExpression(code);
        return expression.Evaluate(context);
    }

    public IList<FactResult> SearchAll(string code)
    {
        var context = new Context(this);
        var call = Loader.LoadCall(code);
        if (context.ResolveSchema(call.Functor, out var schema))
        {
            return FactSearch.SearchAll(context, schema, call.Arguments);
        }

        throw new EvaluateException($"unrecognized schema '{call.Functor}'");
    }
}