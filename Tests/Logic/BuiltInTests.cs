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
}