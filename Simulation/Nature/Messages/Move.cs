using Fictoria.Domain.Locality;

namespace Fictoria.Simulation.Nature.Messages;

public class Move
{
    public string Id { get; }
    public Point Point { get; }

    public Move(string id, double x, double y)
    {
        Id = id;
        Point = new Point(x, y);
    }

    public Move(string id, Point point)
    {
        Id = id;
        Point = point;
    }
}