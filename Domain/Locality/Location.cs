using RBush;

namespace Fictoria.Domain.Locality;

public class Location : ISpatialData, IEquatable<Location>
{
    private readonly Envelope _envelope;

    public string Id { get; }
    public double X { get; }
    public double Y { get; }

    public Location(string id, double x, double y)
    {
        _envelope = new Envelope(x, y, x, y);

        Id = id;
        X = x;
        Y = y;
    }

    public bool Equals(Location? other)
    {
        Console.WriteLine("equals");
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

    public override bool Equals(object? obj)
    {
        return Equals(obj as Location);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}