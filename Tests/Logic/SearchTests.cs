using Fictoria.Logic;
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
                   instance(foo, thing).
                   instance(bar, thing).
                   instance(baz, object).

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
                   instance(foo, thing).
                   instance(bar, thing).
                   instance(baz, object).

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
                   instance(foo, thing).
                   instance(bar, thing).
                   instance(baz, object).

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
                   instance(foo, thing).
                   instance(bar, thing).
                   instance(baz, concept).

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
                   instance(foo, thing).
                   instance(bar, thing).
                   instance(baz, concept).

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
                   instance(foo, thing).
                   instance(bar, thing).
                   instance(baz, concept).

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
}