using Fictoria.Logic;

namespace Fictoria.Tests.Utilities;

public static class Evaluation
{
    public static void AssertEvaluationResult(this Program program, string expression, object expected)
    {
        var result = program.Evaluate(expression);
        Assert.That(result, Is.EqualTo(expected));
    }
}