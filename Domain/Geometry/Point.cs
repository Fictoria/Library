namespace Fictoria.Domain.Geometry;

public class Point : Geometry
{
    public float X { get; }
    public float Y { get; }

    public Point(float x, float y)
    {
        X = x;
        Y = y;
    }

    public bool Contains(Geometry geometry)
    {
        return false;
    }
}