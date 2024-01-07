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
    
    [Test]
    public void Recursion()
    {
        var code = """
                   fib(n: int) =
                       if (n <= 1) {
                           ret = 1
                       } else {
                           ret = fib(n - 1) + fib(n - 2)
                       }; ret.
                   """;
        var program = Loader.Load(code);
        
        program.AssertEvaluationResult("fib(0)", 1);
        program.AssertEvaluationResult("fib(1)", 1);
        program.AssertEvaluationResult("fib(2)", 2);
        program.AssertEvaluationResult("fib(3)", 3);
        program.AssertEvaluationResult("fib(4)", 5);
        program.AssertEvaluationResult("fib(5)", 8);
        program.AssertEvaluationResult("fib(6)", 13);
        program.AssertEvaluationResult("fib(7)", 21);
        program.AssertEvaluationResult("fib(8)", 34);
        program.AssertEvaluationResult("fib(9)", 55);
        program.AssertEvaluationResult("fib(10)", 89);
    }
}