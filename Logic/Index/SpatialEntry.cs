using Fictoria.Domain.Locality;
using RBush;

namespace Fictoria.Logic.Index;

public class SpatialEntry : ISpatialData, IEquatable<SpatialEntry>
{
    private readonly Envelope _envelope;

    public string Id { get; }
    public Point Point { get; }

    public SpatialEntry(string id, Point point)
    {
        _envelope = new Envelope(point.X, point.Y, point.X, point.Y);

        Id = id;
        Point = point;
    }

    public bool Equals(SpatialEntry? other)
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

    public override bool Equals(object? obj)
    {
        return Equals(obj as SpatialEntry);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}