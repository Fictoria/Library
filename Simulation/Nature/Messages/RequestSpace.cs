using Fictoria.Domain.Locality;

namespace Fictoria.Simulation.Nature.Messages;

public class RequestSpace
{
    public Point Point { get; }
    public double Distance { get; }

    public RequestSpace(Point point, double distance)
    {
        Point = point;
        Distance = distance;
    }
}