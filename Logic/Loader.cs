using Antlr4.Runtime;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Lexer;
using Fictoria.Logic.Parser;

namespace Fictoria.Logic;

public class Loader
{
    public static Program Load(string code)
    {
        var program = LoadUnlinked(code);
        Linker.LinkAll(program.Scope);
        return program;
    }

    private static LogicParser makeParser(string code)
    {
        var inputStream = new AntlrInputStream(code);
        var lexer = new LogicLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);
        return new LogicParser(tokens);
    }
    
    public static Program LoadUnlinked(string code)
    {
        var parser = makeParser(code);
        var ast = parser.logic();
        var visitor = new Parser.Parser();
        
        return (Program)visitor.Visit(ast);
    }

    public static Expression.Expression LoadExpression(string code)
    {
        var parser = makeParser(code);
        var ast = parser.expression();
        var visitor = new Parser.Parser();
        
        return (Expression.Expression)visitor.Visit(ast);
    }

    public static Series LoadSeries(string code)
    {
        var parser = makeParser(code);
        var ast = parser.series();
        var visitor = new Parser.Parser();
        
        return (Series)visitor.Visit(ast);
    }

    public static Call LoadCall(string code)
    {
        var parser = makeParser(code);
        var ast = parser.call();
        var visitor = new Parser.Parser();
        
        return (Call)visitor.Visit(ast);
    }
}