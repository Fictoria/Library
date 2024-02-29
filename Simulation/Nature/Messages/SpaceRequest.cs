using Fictoria.Domain.Locality;

namespace Fictoria.Simulation.Nature.Messages;

public class SpaceRequest
{
    public Point Point { get; }
    public double Distance { get; }

    public SpaceRequest(Point point, double distance)
    {
        Point = point;
        Distance = distance;
    }
}