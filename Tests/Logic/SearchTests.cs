using Fictoria.Logic;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Fact;
using Fictoria.Tests.Utilities;

namespace Fictoria.Tests.Logic;

public class SearchTests
{
    [Test]
    public void Match()
    {
        var code = """
                   thing: object.
                   instantiate(foo, thing).
                   instantiate(bar, thing).
                   instantiate(baz, object).

                   exists(t: thing).
                   exists(foo).
                   exists(bar).
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("exists(foo)", true);
        program.AssertEvaluationResult("exists(bar)", true);
        program.AssertEvaluationResult("exists(baz)", false);
    }

    [Test]
    public void MatchWildcard()
    {
        var code = """
                   thing: object.
                   instantiate(foo, thing).
                   instantiate(bar, thing).
                   instantiate(baz, object).

                   exists(t: thing).
                   exists(foo).
                   exists(bar).
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("exists(_)", true);
    }

    [Test]
    public void MatchBind()
    {
        var code = """
                   thing: object.
                   instantiate(foo, thing).
                   instantiate(bar, thing).
                   instantiate(baz, object).

                   exists(t: thing).
                   exists(foo).
                   exists(bar).
                   """;
        var program = Loader.Load(code);
        var foo = program.Scope.Instances["foo"];

        program.AssertEvaluationResult("exists(@e); e", foo);
    }

    [Test]
    public void MatchBindFilter()
    {
        var code = """
                   thing: object.
                   concept: object.
                   instantiate(foo, thing).
                   instantiate(bar, thing).
                   instantiate(baz, concept).

                   exists(t: thing).
                   exists(foo).
                   exists(bar).
                   exists(baz).
                   """;
        var program = Loader.Load(code);
        var baz = program.Scope.Instances["baz"];

        program.AssertEvaluationResult("exists(@e :: concept); e", baz);
    }

    [Test]
    public void MatchBindMany()
    {
        var code = """
                   thing: object.
                   concept: object.
                   instantiate(foo, thing).
                   instantiate(bar, thing).
                   instantiate(baz, concept).

                   exists(t: object).
                   exists(foo).
                   exists(bar).
                   exists(baz).
                   """;
        var program = Loader.Load(code);
        var foo = program.Scope.Instances["foo"];
        var bar = program.Scope.Instances["bar"];
        var baz = program.Scope.Instances["baz"];

        program.AssertEvaluationResult("&exists(@all); all", new List<Instance>
        {
            foo, bar, baz
        });
    }

    [Test]
    public void MatchBindManyFilter()
    {
        var code = """
                   thing: object.
                   concept: object.
                   instantiate(foo, thing).
                   instantiate(bar, thing).
                   instantiate(baz, concept).

                   exists(t: object).
                   exists(foo).
                   exists(bar).
                   exists(baz).
                   """;
        var program = Loader.Load(code);
        var foo = program.Scope.Instances["foo"];
        var bar = program.Scope.Instances["bar"];

        program.AssertEvaluationResult("&exists(@all :: thing); all", new List<Instance>
        {
            foo, bar
        });
    }

    [Test]
    public void MatchInvariant()
    {
        var code = """
                   thing: object.
                   tool: thing.
                   knife: tool.

                   exists(o: object).
                   exists(knife).
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("exists(knife)", true);
        program.AssertEvaluationResult("exists(tool)", false);
    }

    [Test]
    public void MatchCovariant()
    {
        var code = """
                   thing: object.
                   tool: thing.
                   knife: tool.

                   exists(o: +object).
                   exists(tool).
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("exists(knife)", true);
    }

    [Test]
    public void MatchCovariantInstance()
    {
        var code = """
                   thing: object.
                   tool: thing.
                   knife: tool.
                   instantiate(knife_1, knife).

                   exists(o: +object).
                   exists(tool).
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("exists(knife_1)", true);
    }

    [Test]
    public void MatchContravariant()
    {
        var code = """
                   thing: object.
                   tool: thing.
                   knife: tool.

                   carrying(o: -object).
                   carrying(knife).
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("carrying(tool)", true);
    }

    [Test]
    public void MatchContravariantInstance()
    {
        var code = """
                   thing: object.
                   tool: thing.
                   knife: tool.
                   instantiate(knife_1, knife).

                   carrying(o: -object).
                   carrying(knife_1).
                   """;
        var program = Loader.Load(code);

        program.AssertEvaluationResult("carrying(tool)", true);
    }

    [Test]
    public void InvalidArguments()
    {
        var code = """
                   thing: object.
                   instantiate(tool, thing).
                   carrying(o: object).
                   """;
        var program = Loader.Load(code);

        Assert.Throws<LinkException>(() => program.Evaluate("carrying(true, true)"));
    }
}