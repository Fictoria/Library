using Fictoria.Logic;
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
                   instantiate(dog, thing).

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
                   instantiate(dog, thing).

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
                   instantiate(dog, thing).

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

        program.AssertEvaluationResult("f(4)", new List<object> { 1, 2, 3, 4 });
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

        program.AssertEvaluationResult("f()", new Dictionary<string, object>
        {
            { "foo", new Dictionary<string, object> { { "bar", "baz" } } }
        });
    }

    [Test]
    public void Unary()
    {
        var code = """
                   f() = -1.
                   g() = -1.0.
                   h() = !true.
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("f()", -1);
        program.AssertEvaluationResult("g()", -1.0);
        program.AssertEvaluationResult("h()", false);
    }

    [Test]
    public void TypeIsA()
    {
        var code = """
                   thing: object.
                   animal: thing.
                   plant: thing.
                   instantiate(foo, thing).
                   instantiate(bar, animal).

                   exists(o: object).
                   exists(foo).
                   exists(bar).

                   f() = exists(@b :: animal).
                   g() = exists(@b :: plant).
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("f()", true);
        program.AssertEvaluationResult("g()", false);
    }

    [Test]
    public void Exponent()
    {
        var code = """
                   f() = 2^3.
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("f()", 8);
    }

    [Test]
    public void Zip()
    {
        var code = """
                   f() = [1, 2, 3] ~ ["a", "b", "c"].
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("f()", new List<object>
        {
            new List<object> { 1, "a" },
            new List<object> { 2, "b" },
            new List<object> { 3, "c" }
        });
    }

    [Test]
    public void Cartesian()
    {
        var code = """
                   f() = [1, 2, 3] * ["a", "b", "c"].
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("f()", new List<object>
        {
            new List<object> { 1, "a" },
            new List<object> { 1, "b" },
            new List<object> { 1, "c" },
            new List<object> { 2, "a" },
            new List<object> { 2, "b" },
            new List<object> { 2, "c" },
            new List<object> { 3, "a" },
            new List<object> { 3, "b" },
            new List<object> { 3, "c" }
        });
    }

    [Test]
    public void Append()
    {
        var code = """
                   f() = [1, 2, 3] + ["a", "b", "c"].
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("f()", new List<object>
        {
            1, 2, 3, "a", "b", "c"
        });
    }

    [Test]
    public void Math()
    {
        var code = """
                   f(x: int) = x + 1 - 1 * 1 / 1.
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("f(2)", 2);
    }

    [Test]
    public void Comparison()
    {
        var code = """
                   eq(a: int, b: int) = a == b.
                   ne(a: int, b: int) = a != b.
                   gt(a: int, b: int) = a > b.
                   gte(a: int, b: int) = a >= b.
                   lt(a: int, b: int) = a < b.
                   lte(a: int, b: int) = a <= b.
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("eq(1, 1)", true);
        program.AssertEvaluationResult("eq(1, 2)", false);
        program.AssertEvaluationResult("ne(1, 1)", false);
        program.AssertEvaluationResult("ne(1, 2)", true);
        program.AssertEvaluationResult("gt(1, 1)", false);
        program.AssertEvaluationResult("gt(1, 2)", false);
        program.AssertEvaluationResult("gt(2, 1)", true);
        program.AssertEvaluationResult("gte(1, 1)", true);
        program.AssertEvaluationResult("gte(1, 2)", false);
        program.AssertEvaluationResult("gte(2, 1", true);
        program.AssertEvaluationResult("lt(1, 1)", false);
        program.AssertEvaluationResult("lt(1, 2)", true);
        program.AssertEvaluationResult("lt(2, 1)", false);
        program.AssertEvaluationResult("lte(1, 1)", true);
        program.AssertEvaluationResult("lte(1, 2)", true);
        program.AssertEvaluationResult("lte(2, 1", false);
    }

    [Test]
    public void Boolean()
    {
        var code = """
                   a(x: bool, y: bool) = x and y.
                   o(x: bool, y: bool) = x or y.
                   xo(x: bool, y: bool) = x xor y.
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("a(true, true)", true);
        program.AssertEvaluationResult("a(true, false)", false);
        program.AssertEvaluationResult("a(false, true)", false);
        program.AssertEvaluationResult("a(false, false)", false);

        program.AssertEvaluationResult("o(true, true)", true);
        program.AssertEvaluationResult("o(true, false)", true);
        program.AssertEvaluationResult("o(false, true)", true);
        program.AssertEvaluationResult("o(false, false)", false);

        program.AssertEvaluationResult("xo(true, true)", false);
        program.AssertEvaluationResult("xo(true, false)", true);
        program.AssertEvaluationResult("xo(false, true)", true);
        program.AssertEvaluationResult("xo(false, false)", false);
    }

    [Test]
    public void Index()
    {
        var code = """
                   f(key: string) = { "foo": "bar". }[key].
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("""f("foo")""", "bar");
    }

    [Test]
    public void Concatenate()
    {
        var code = """
                   concat(a: string, b: string) = a + b.
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("""concat("foo", "bar")""", "foobar");
    }
}