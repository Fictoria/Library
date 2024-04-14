using Fictoria.Domain.Locality;

namespace Fictoria.Simulation.Nature.Messages;

public class Disappear
{
    public string Id { get; }
    public Point Point { get; }

    public Disappear(string id, double x, double y)
    {
        Id = id;
        Point = new Point(x, y);
    }

    public Disappear(string id, Point point)
    {
        Id = id;
        Point = point;
    }
}