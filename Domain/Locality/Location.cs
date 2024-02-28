using RBush;

namespace Fictoria.Domain.Locality;

public class Location : ISpatialData, IEquatable<Location>
{
    private readonly Envelope _envelope;

    public string Id { get; }
    public Point Point { get; }

    public Location(string id, double x, double y)
    {
        _envelope = new Envelope(x, y, x, y);

        Id = id;
        Point = new Point(x, y);
    }

    public Location(string id, Point point)
    {
        _envelope = new Envelope(point.X, point.Y, point.X, point.Y);

        Id = id;
        Point = point;
    }

    public bool Equals(Location? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return other.Id == Id;
    }

    public ref readonly Envelope Envelope => ref _envelope;

    public double DistanceTo(double x, double y)
    {
        return Point.DistanceTo(x, y);
    }

    public double DistanceTo(Point point)
    {
        return Point.DistanceTo(point.X, point.Y);
    }

    public double DistanceTo(Location location)
    {
        return DistanceTo(location.Point);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Location);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}