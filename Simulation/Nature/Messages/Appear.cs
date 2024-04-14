using Fictoria.Domain.Locality;

namespace Fictoria.Simulation.Nature.Messages;

public class Appear
{
    public string Id { get; }
    public Point Point { get; }

    public Appear(string id, double x, double y)
    {
        Id = id;
        Point = new Point(x, y);
    }

    public Appear(string id, Point point)
    {
        Id = id;
        Point = point;
    }
}