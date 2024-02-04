using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Parser;

namespace Fictoria.Logic;

public class Program
{
    public Program(Scope scope)
    {
        Scope = scope;
    }

    public Scope Scope { get; }

    public object Evaluate(string code)
    {
        var context = new Context(this);
        var expression = Loader.LoadSeries(code);
        Linker.LinkExpression(Scope, expression);
        return expression.Evaluate(context);
    }

    public object Evaluate(Expression.Expression expression)
    {
        var context = new Context(this);
        Linker.LinkExpression(Scope, expression);
        return expression.Evaluate(context);
    }

    public object Evaluate(Expression.Expression expression, Dictionary<string, object> bindings)
    {
        var context = new Context(this);
        return Evaluate(context, expression, bindings);
    }

    public object Evaluate(Context context, Expression.Expression expression, Dictionary<string, object> bindings)
    {
        foreach (var (k, v) in bindings)
        {
            context.Bind(k, v);
            // Scope.Bindings[k] = v;
        }

        Linker.LinkExpression(Scope, expression);
        return expression.Evaluate(context);
    }

    public void Merge(string code)
    {
        var other = Loader.LoadUnlinked(code).Scope;
        Scope.Merge(other);
        Linker.LinkAll(Scope);
    }

    public void Merge(Scope other)
    {
        var clone = new Scope(other);
        Scope.Merge(clone);
    }

    public Program Clone()
    {
        return new Program(new Scope(Scope));
    }
}