using Fictoria.Logic;
using Fictoria.Logic.Fact;
using Fictoria.Tests.Utilities;

namespace Fictoria.Tests.Logic;

public class ExpressionTests
{
    [Test]
    public void Literals()
    {
        var code = """
                   a() = 1.
                   b() = 1.1.
                   c() = true.
                   d() = "foobar".
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("a()", 1);
        program.AssertEvaluationResult("b()", 1.1);
        program.AssertEvaluationResult("c()", true);
        program.AssertEvaluationResult("d()", "foobar");
    }
    
    [Test]
    public void Identifier()
    {
        var code = """
                   f(x: int) = x.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f(123)", 123);
    }
    
    [Test]
    public void Search()
    {
        var code = """
                   thing: object.
                   instance(dog, thing).

                   legs(t: thing, n: int).
                   legs(dog, 4).

                   f(x: int) = legs(dog, x).
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f(3)", false);
        program.AssertEvaluationResult("f(4)", true);
        program.AssertEvaluationResult("f(5)", false);
    }
    
    [Test]
    public void Wildcard()
    {
        var code = """
                   thing: object.
                   instance(dog, thing).

                   legs(t: thing, n: int).
                   legs(dog, 4).

                   f(x: int) = legs(_, x).
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f(3)", false);
        program.AssertEvaluationResult("f(4)", true);
        program.AssertEvaluationResult("f(5)", false);
    }
    
    [Test]
    public void Binding()
    {
        var code = """
                   thing: object.
                   instance(dog, thing).

                   legs(t: thing, n: int).
                   legs(dog, 4).

                   f(x: int) = legs(@name, x); name.
                   """;
        var program = Loader.Load(code);
        var dog = program.Scope.Instances["dog"];
        
        program.AssertEvaluationResult("f(4)", dog);
    }
    
    [Test]
    public void Parenthetical()
    {
        var code = """
                   f(x: int) = x + (x - x) * x.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f(4)", 4);
    }
    
    [Test]
    public void Tuple()
    {
        var code = """
                   f(x: int) = [1, 2, 3, x].
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f(4)", new List<object> {1, 2, 3, 4});
    }
    
    [Test]
    public void Call()
    {
        var code = """
                   pow(b: int, e: int) = b ^ e.
                   cube(n: int) = pow(n, 3).
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("cube(5)", 125);
    }
    
    [Test]
    public void Assign()
    {
        var code = """
                   f(x: int) =
                       double = x * 2;
                       double_that = double * 2;
                       double_that.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f(5)", 20);
    }
    
    [Test]
    public void If()
    {
        // TODO an if-ladder should evaluate to the block's value
        var code = """
                   f(x: int) =
                       if (x <= 0) {
                           r = x
                       } else if (x <= 5) {
                           r = x * 2
                       } else {
                           r = x * 3
                       }; r.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f(-1)", -1);
        program.AssertEvaluationResult("f(0)", 0);
        program.AssertEvaluationResult("f(4)", 8);
        program.AssertEvaluationResult("f(5)", 10);
        program.AssertEvaluationResult("f(15)", 45);
    }
    
    [Test]
    public void Struct()
    {
        // TODO an if-ladder should evaluate to the block's value
        var code = """
                   f() =
                       {
                           "foo": {
                               "bar": "baz".
                           }.
                       }.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f()", new Dictionary<string, object> {
            { "foo", new Dictionary<string, object> { { "bar", "baz" } } }
        });
    }
    
    [Test]
    public void Unary()
    {
        var code = """
                   f() = -1.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f()", -1);
    }
}