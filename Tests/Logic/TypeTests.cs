using Fictoria.Logic;
using Type = Fictoria.Logic.Type.Type;

namespace Fictoria.Tests.Logic;

public class TypeTests
{
    [SetUp]
    public void Setup()
    {
    }

    // var result = (List<object>)program.Evaluate("&searchable(@all); all");
    [Test]
    public void IsA()
    {
        var code = """
                   thing: object.
                   """;
        var program = Loader.Load(code);
        var thing = program.Scope.Types["thing"];
        Assert.True(thing.IsA(Type.Object));
    }

    [Test]
    public void ParentIsA()
    {
        var code = """
                   thing: object.
                   rock: thing.
                   """;
        var program = Loader.Load(code);
        var rock = program.Scope.Types["rock"];
        Assert.True(rock.IsA(Type.Object));
    }
    
    [Test]
    public void MultipleIsA()
    {
        var code = """
                   aspect: object.
                   walking: aspect.
                   flying: aspect.
                   bird: walking, flying.
                   """;
        var program = Loader.Load(code);
        var walking = program.Scope.Types["walking"];
        var flying = program.Scope.Types["flying"];
        var bird = program.Scope.Types["bird"];
        Assert.True(bird.IsA(walking));
        Assert.True(bird.IsA(flying));
    }
    
    [Test]
    public void ParentMultipleIsA()
    {
        var code = """
                   aspect: object.
                   walking: aspect.
                   flying: aspect.
                   bird: walking, flying.
                   sparrow: bird.
                   """;
        var program = Loader.Load(code);
        var walking = program.Scope.Types["walking"];
        var flying = program.Scope.Types["flying"];
        var bird = program.Scope.Types["bird"];
        var sparrow = program.Scope.Types["sparrow"];
        Assert.True(sparrow.IsA(bird));
        Assert.True(sparrow.IsA(walking));
        Assert.True(sparrow.IsA(flying));
    }
}