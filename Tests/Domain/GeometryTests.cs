using Fictoria.Domain.Geometry;

namespace Fictoria.Tests.Domain;

public class GeometryTests
{
    [Test]
    public void PointDistance()
    {
        var point1 = new Point(1.0f, 2.0f);
        var point2 = new Point(3.0f, 4.0f);
        Assert.That(point1.DistanceTo(point2), Is.EqualTo(2.8284271f).Within(0.0001f));
    }
    
    [Test]
    public void PointDistanceNegatives()
    {
        var point1 = new Point(-1.0f, 2.0f);
        var point2 = new Point(3.0f, -4.0f);
        Assert.That(point1.DistanceTo(point2), Is.EqualTo(7.2111026f).Within(0.0001f));
    }
}