namespace Fictoria.Domain.Geometry;

public class Point
{
    public float X { get; }
    public float Y { get; }

    public Point(float x, float y)
    {
        X = x;
        Y = y;
    }

    public double DistanceTo(Point point)
    {
        return Math.Sqrt(Math.Pow(X - point.X, 2) + Math.Pow(Y - point.Y, 2));
    }
}