using Domain;

namespace Planning.Actions;

public class Walk : Action
{
    public Point Target { get; }

    public Walk(Point target)
    {
        Target = target;
    }
}