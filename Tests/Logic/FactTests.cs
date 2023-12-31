using System.Linq;
using Fictoria.Logic;
using Fictoria.Logic.Expression;

namespace Fictoria.Tests.Logic;

public class FactTests
{
    [Test]
    public void Schema()
    {
        var code = """
                   animal: object.
                   cat: animal.
                   dragonfly: animal.

                   legs(a: animal, n: int).
                   """;
        var program = Loader.Load(code);
        var legs = program.Scope.Schemata["legs"];
        
        Assert.That(legs.Parameters, Has.Count.EqualTo(2));
        Assert.That(legs.Parameters[0].Name, Is.EqualTo("a"));
        Assert.That(legs.Parameters[0].Type.Name, Is.EqualTo("animal"));
        Assert.That(legs.Parameters[1].Name, Is.EqualTo("n"));
        Assert.That(legs.Parameters[1].Type.Name, Is.EqualTo("int"));
    }
    
    [Test]
    public void Fact()
    {
        var code = """
                   animal: object.
                   cat: animal.
                   dragonfly: animal.

                   legs(a: animal, n: int).
                   legs(cat, 4).
                   legs(dragonfly, 6).
                   """;
        var program = Loader.Load(code);
        var facts = program.Scope.Facts["legs"];
        var sorted = facts.OrderBy(f => f.Arguments[0].Text).ToList();
        var cat = sorted[0];
        var dragonfly = sorted[1];
        
        Assert.That(facts, Has.Count.EqualTo(2));
        Assert.That(((Literal)cat.Arguments[1]).Value, Is.EqualTo(4));
        Assert.That(((Literal)dragonfly.Arguments[1]).Value, Is.EqualTo(6));
    }
}