namespace Fictoria.Domain.Geometry;

public class QuadtreeEntry
{
    public string Id { get; }
    public Point Point { get; }

    public QuadtreeEntry(string id, Point point)
    {
        Id = id;
        Point = point;
    }
}