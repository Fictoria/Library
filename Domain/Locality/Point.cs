namespace Fictoria.Domain.Locality;

public class Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Point Add(double x, double y)
    {
        return new Point(X + x, Y + y);
    }

    public Point Add(Point point)
    {
        return new Point(X + point.X, Y + point.Y);
    }

    public double DistanceTo(double x2, double y2)
    {
        return Math.Sqrt(Math.Pow(x2 - X, 2) + Math.Pow(y2 - Y, 2));
    }

    public double DistanceTo(Point point)
    {
        return DistanceTo(point.X, point.Y);
    }

    public double DirectionTo(double x2, double y2)
    {
        return Math.Atan2(y2 - Y, x2 - X);
    }

    public double DirectionTo(Point point)
    {
        return DirectionTo(point.X, point.Y);
    }
}