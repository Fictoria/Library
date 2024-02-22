namespace Fictoria.Domain.Geometry;

public class Rectangle
{
    public Point Origin { get; }
    public float Width { get; }
    public float Height { get; }
    public Point Center => new(Origin.X + Width / 2, Origin.Y + Height / 2);

    public Rectangle(Point origin, float width, float height)
    {
        Origin = origin;
        Width = width;
        Height = height;
    }

    public bool Contains(Point point)
    {
        return point.X >= Origin.X && point.X <= Origin.X + Width &&
               point.Y >= Origin.Y && point.Y <= Origin.Y + Height;
    }
}