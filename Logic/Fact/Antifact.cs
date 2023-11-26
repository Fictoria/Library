using Fictoria.Logic.Expression;

namespace Fictoria.Logic.Fact;

public class Antifact
{
    public Call Matcher { get; }

    public Antifact(Call matcher)
    {
        Matcher = matcher;
    }
}