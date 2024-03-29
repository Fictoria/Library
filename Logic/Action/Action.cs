using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Action;

public class Action
{
    public string Name { get; }
    public IList<Parameter> Parameters { get; }
    public Expression.Expression Space { get; }
    public Expression.Expression Cost { get; }
    public Expression.Expression Conditions { get; }
    public Expression.Expression? Target { get; }
    public Expression.Expression? Locals { get; }
    public Expression.Expression Terms { get; }
    public Scope Effects { get; } // TODO generalize statements?

    public Action(
        string name,
        IList<Parameter> parameters,
        Expression.Expression space,
        Expression.Expression cost,
        Expression.Expression conditions,
        Expression.Expression? target,
        Expression.Expression? locals,
        Expression.Expression terms,
        Scope effects)
    {
        Name = name;
        Parameters = parameters;
        Space = space;
        Cost = cost;
        Conditions = conditions;
        Target = target;
        Locals = locals;
        Terms = terms;
        Effects = effects;
    }
}