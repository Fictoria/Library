using Action = Fictoria.Logic.Action.Action;

namespace Fictoria.Planning.Planner;

public class Step
{
    public Step(Action action, Dictionary<string, object> bindings)
    {
        Action = action;
        Bindings = bindings;
    }

    public Action Action { get; }
    public Dictionary<string, object> Bindings { get; }
}