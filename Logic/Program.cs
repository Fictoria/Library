using Fictoria.Logic.Evaluation;

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
}