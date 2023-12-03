using Fictoria.Logic.Type;

namespace Fictoria.Logic.Action;

public class Action
{
    public string Name { get; }
    public IList<Parameter> Parameters { get; }
    public Expression.Expression Space { get; }
    public Expression.Expression Cost { get; }
    public Expression.Expression Conditions { get; }
    public Expression.Expression Locals { get; }
    public IList<object> Effects { get; } //TODO generalize statements?

    public Action(
        string name,
        IList<Parameter> parameters,
        Expression.Expression space,
        Expression.Expression cost,
        Expression.Expression conditions,
        Expression.Expression locals,
        IList<object> effects)
    {
        Name = name;
        Parameters = parameters;
        Space = space;
        Cost = cost;
        Conditions = conditions;
        Locals = locals;
        Effects = effects;
    }
}