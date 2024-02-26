using Fictoria.Domain.Locality;

namespace Fictoria.Tests.Domain;

#pragma warning disable NUnit2009
public class LocalityTests
{
    [Test]
    public void Location()
    {
        var x = new Location("abc", 1, 2);
        var y = new Location("abc", 3, 4);
        var z = new Location("xyz", 0, 0);

        Assert.That(x, Is.EqualTo(x));
        Assert.That(y, Is.EqualTo(y));
        Assert.That(y, Is.EqualTo(x));
        Assert.That(x, Is.EqualTo(y));

        Assert.That(x, Is.Not.EqualTo(z));
        Assert.That(y, Is.Not.EqualTo(z));
    }

    [Test]
    public void ConstellationInsert()
    {
        var tree = new Constellation();
        Assert.That(tree.Count, Is.EqualTo(0));

        tree.Insert("abc", 1, 2);
        Assert.That(tree.Count, Is.EqualTo(1));

        tree.Insert(new Location("xyz", 0, 0));
        Assert.That(tree.Count, Is.EqualTo(2));
    }

    [Test]
    public void ConstellationRemove()
    {
        var tree = new Constellation();
        var location = new Location("xyz", 0, 0);

        tree.Insert(location);
        Assert.That(tree.Count, Is.EqualTo(1));

        tree.Remove(location);
        Assert.That(tree.Count, Is.EqualTo(0));

        tree.Insert(location);
        Assert.That(tree.Count, Is.EqualTo(1));

        tree.Remove("xyz");
        Assert.That(tree.Count, Is.EqualTo(0));
    }

    [Test]
    public void ConstellationUpdate()
    {
        var tree = new Constellation();
        Assert.That(tree.Count, Is.EqualTo(0));

        var location = new Location("abc", 1, 2);
        tree.Insert(location);
        Assert.That(tree.Count, Is.EqualTo(1));

        tree.Update("abc", 4, 5);
        Assert.That(tree.Count, Is.EqualTo(1));
    }

    [Test]
    public void ConstellationSearch()
    {
        var tree = new Constellation();
        var x = new Location("abc", 1, 2);
        var y = new Location("def", 3, 4);
        var z = new Location("ghi", 0, 0);
        var a = new Location("ghi", 100, 100);
        
        tree.Insert(x);
        tree.Insert(y);
        tree.Insert(z);
        tree.Insert(a);

        var results = tree.Search(0, 0, 10);
        Assert.That(results.Count, Is.EqualTo(3));
    }
}