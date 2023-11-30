using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class If : Expression
{
    public Expression Condition { get; }
    public Expression Body { get; }
    public IList<If> ElseIfs { get; }
    public Expression? Else { get; }

    public If(string text, Expression condition, Expression body, IList<If> elseIfs, Expression? _else) : base(text, Logic.Type.Type.Boolean)
    {
        Condition = condition;
        Body = body;
        ElseIfs = elseIfs;
        Else = _else;
    }

    public override object Evaluate(Context context)
    {
        var challenge = Condition.Evaluate(context);
        if (challenge is true)
        {
            Body.Evaluate(context);
            return true;
        }

        foreach (var ei in ElseIfs)
        {
            var r = (bool?)ei.Condition.Evaluate(context);
            if (r is true)
            {
                ei.Evaluate(context);
                return true;
            }
        }

        var e = Else?.Evaluate(context);
        return true;
    }

    public override IEnumerable<string> Terms()
    {
        var terms = new HashSet<string>();
        Condition.Terms().ToList().ForEach(t => terms.Add(t));
        Body.Terms().ToList().ForEach(t => terms.Add(t));
        ElseIfs.ToList().ForEach(ei => ei.Terms().ToList().ForEach(t => terms.Add(t)));
        Else?.Terms().ToList().ForEach(t => terms.Add(t));
        return terms;
    }
}