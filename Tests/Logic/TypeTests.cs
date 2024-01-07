using Fictoria.Logic;
using Type = Fictoria.Logic.Type.Type;

namespace Fictoria.Tests.Logic;

public class TypeTests
{
    [Test]
    public void Identity()
    {
        Assert.True(Type.Object.IsA(Type.Object));
    }
    
    [Test]
    public void Anything()
    {
        Assert.True(Type.Object.IsA(Type.Anything));
        Assert.True(Type.Anything.IsA(Type.Object));
    }
    
    [Test]
    public void Nothing()
    {
        Assert.False(Type.Object.IsA(Type.Nothing));
        Assert.False(Type.Nothing.IsA(Type.Object));
    }

    [Test]
    public void IsA()
    {
        var code = """
                   thing: object.
                   """;
        var program = Loader.Load(code);
        var thing = program.Scope.Types["thing"];
        
        Assert.True(thing.IsA(Type.Object));
        Assert.False(thing.IsA(Type.Struct));
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
        Assert.False(Type.Object.IsA(rock));
    }

    [Test]
    public void GrandparentIsA()
    {
        var code = """
                   thing: object.
                   rock: thing.
                   igneous: rock.
                   """;
        var program = Loader.Load(code);
        var igneous = program.Scope.Types["igneous"];
        
        Assert.True(igneous.IsA(Type.Object));
        Assert.False(Type.Object.IsA(igneous));
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
        Assert.False(walking.IsA(bird));
        Assert.True(bird.IsA(flying));
        Assert.False(flying.IsA(bird));
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
        Assert.False(bird.IsA(sparrow));
        Assert.True(sparrow.IsA(walking));
        Assert.False(walking.IsA(sparrow));
        Assert.True(sparrow.IsA(flying));
        Assert.False(flying.IsA(sparrow));
    }

    [Test]
    public void Instance()
    {
        var code = """
                   dog: object.
                   instance(spot, dog).
                   instance(fido, dog).
                   """;
        var program = Loader.Load(code);
        var dog = program.Scope.Types["dog"];
        var spot = program.Scope.Instances["spot"];
        var fido = program.Scope.Instances["fido"];
        Assert.True(spot.Type.IsA(dog));
        Assert.True(fido.Type.IsA(dog));
    }
}