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
}