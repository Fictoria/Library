namespace Fictoria.Logic.Expression;

public class Search : Infix
{
    public Binding Binding { get; }
    
    public Search(string text, Binding left, string op, Expression right)
        : base(text, new Identifier("$", "$"), op, right)
    {
        Binding = left;
    }
}