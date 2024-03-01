using Fictoria.Domain.Locality;

namespace Fictoria.Logic.Index;

public class Using
{
    public Point Point { get; }
    public double Distance { get; }

    public Using(Point point, double distance)
    {
        Point = point;
        Distance = distance;
    }
}