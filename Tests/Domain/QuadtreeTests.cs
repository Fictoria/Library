using Fictoria.Domain.Geometry;

namespace Fictoria.Tests.Domain;

public class QuadtreeTests
{
    [Test]
    public void Insert()
    {
        var bounds = new Rectangle(new Point(0.0f, 0.0f), 10.0f, 10.0f);
        var point1 = new Point(1.0f, 2.0f);
        var point2 = new Point(3.0f, 4.0f);
        var point3 = new Point(5.0f, 6.0f);

        var quadtree = new Quadtree(bounds, 10, 10);
        quadtree.Insert("1", point1);
        quadtree.Insert("2", point2);
        quadtree.Insert("3", point3);
        Assert.That(quadtree.Count(), Is.EqualTo(3));
    }

    [Test]
    public void Remove()
    {
        var bounds = new Rectangle(new Point(0.0f, 0.0f), 10.0f, 10.0f);
        var point1 = new Point(1.0f, 2.0f);
        var point2 = new Point(3.0f, 4.0f);
        var point3 = new Point(5.0f, 6.0f);

        var quadtree = new Quadtree(bounds, 10, 10);
        quadtree.Insert("1", point1);
        quadtree.Insert("2", point2);
        quadtree.Insert("3", point3);
        Assert.That(quadtree.Count(), Is.EqualTo(3));
        quadtree.Remove("3");
        Assert.That(quadtree.Count(), Is.EqualTo(2));
    }
}