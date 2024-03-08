using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Expression;

public class Lambda : Expression
{
    private readonly Function.Function _function;
    public List<Parameter> Parameters { get; }
    public Expression Implementation { get; }

    public Lambda(string text, List<Parameter> parameters, Expression expression) : base(text,
        Logic.Type.Type.Function)
    {
        Implementation = expression;
        Parameters = parameters;
        _function = new Function.Function("<lambda>", parameters, expression);
    }

    public override object Evaluate(Context context)
    {
        return _function;
    }

    public override IEnumerable<string> Terms()
    {
        return new string[] { };
    }
}