using Fictoria.Domain.Locality;

namespace Fictoria.Simulation.Human.Messages;

public class Walk
{
    public Point Point { get; }

    public Walk(Point point)
    {
        Point = point;
    }
}