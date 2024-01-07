using Fictoria.Logic;
using Fictoria.Tests.Utilities;

namespace Fictoria.Tests.Logic;

public class FunctionTests
{
    [Test]
    public void Constant()
    {
        var code = """
                   f() = 42.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f()", 42);
    }
    
    [Test]
    public void Arithmetic()
    {
        var code = """
                   double(x: int) = x + x.
                   halve(x: int) = x / 2.
                   square(x: int) = x ^ 2.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("double(2)", 4);
        program.AssertEvaluationResult("halve(10)", 5);
        program.AssertEvaluationResult("square(5)", 25);
    }
    
    [Test]
    public void Arguments()
    {
        var code = """
                   add(a: int, b: int) = a + b.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("add(2, 3)", 5);
    }
    
    [Test]
    public void ReturnsLast()
    {
        var code = """
                   average(a: int, b: int) =
                       sum = a + b;
                       halfSum = sum / 2;
                       halfSum.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("average(2, 4)", 3);
    }
    
    [Test]
    public void Compose()
    {
        var code = """
                   f(x: int) = x * 2.
                   g(x: int) = x ^ 2.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("f(f(5))", 20);
        program.AssertEvaluationResult("f(g(5))", 50);
        program.AssertEvaluationResult("g(f(5))", 100);
    }
}