using Fictoria.Logic;

namespace Fictoria.Tests.Logic;

public class BuiltInTests
{
    [Test]
    public void Typeof()
    {
        var code = """
                   foo: object.
                   instantiate(bar, foo).
                   """;
        var program = Loader.Load(code);
        var type = program.Evaluate("typeof(bar)");

        Assert.That(type, Is.EqualTo(program.Scope.Types["foo"]));
    }

    [Test]
    public void Zip()
    {
        var code = """
                   f() = [1,2].
                   g() = [4,5].
                   h() = [7,8].
                   """;
        var program = Loader.Load(code);
        var result = (List<object>)program.Evaluate("zip(f(), g(), h())");
        var row1 = (List<object>)result[0];
        var row2 = (List<object>)result[1];

        Assert.That(row1[0], Is.EqualTo(1));
        Assert.That(row1[1], Is.EqualTo(4));
        Assert.That(row1[2], Is.EqualTo(7));

        Assert.That(row2[0], Is.EqualTo(2));
        Assert.That(row2[1], Is.EqualTo(5));
        Assert.That(row2[2], Is.EqualTo(8));
    }

    [Test]
    public void Sort()
    {
        var code = """
                   f() = [2,1,5,4,-2].
                   g(x: int) = x.
                   """;
        var program = Loader.Load(code);
        var result = (List<object>)program.Evaluate("sort(f(), g)");
        Assert.That(result[0], Is.EqualTo(-2));
        Assert.That(result[1], Is.EqualTo(1));
        Assert.That(result[2], Is.EqualTo(2));
        Assert.That(result[3], Is.EqualTo(4));
        Assert.That(result[4], Is.EqualTo(5));
    }

    [Test]
    public void Map()
    {
        var code = """
                   f() = [2,1,5,4,-2].
                   g() = map(f(), (x: int) => x * x).
                   """;
        var program = Loader.Load(code);
        var result = (List<object>)program.Evaluate("g()");
        Assert.That(result[0], Is.EqualTo(4));
        Assert.That(result[1], Is.EqualTo(1));
        Assert.That(result[2], Is.EqualTo(25));
        Assert.That(result[3], Is.EqualTo(16));
        Assert.That(result[4], Is.EqualTo(4));
    }

    [Test]
    public void Filter()
    {
        var code = """
                   f() = [2,1,5,4,-2].
                   g() = filter(f(), (x: int) => x > 0).
                   """;
        var program = Loader.Load(code);
        var result = (List<object>)program.Evaluate("g()");
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(2));
        Assert.That(result[1], Is.EqualTo(1));
        Assert.That(result[2], Is.EqualTo(5));
        Assert.That(result[3], Is.EqualTo(4));
    }

    [Test]
    public void AngularDiameter()
    {
        var code = """
                   angular_diameter(dist: float, diam: float) =
                       2.0 * arctan(diam / (2.0 * dist)).
                   """;
        var program = Loader.Load(code);
        var result = program.Evaluate("angular_diameter(100.0, 0.3)");
        Assert.That(result, Is.EqualTo(0.003).Within(0.0001));
    }

    [Test]
    public void Clamp()
    {
        var program = Loader.Load("x:object.");
        //Positive Case
        var result = program.Evaluate("clamp(3,1,5)");
        Assert.That(result, Is.EqualTo(3));
        //Negative Case
        result = program.Evaluate("clamp(3,1,2)");
        Assert.That(result, Is.EqualTo(2));
    }
}