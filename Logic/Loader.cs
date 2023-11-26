using Antlr4.Runtime;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Lexer;
using Fictoria.Logic.Parser;

namespace Fictoria.Logic;

public class Loader
{
    public static Program Load(string code)
    {
        var program = load(code);
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
    
    private static Program load(string code)
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

    public static Fact.Fact LoadFact(string code)
    {
        var parser = makeParser(code);
        var ast = parser.fact();
        var visitor = new Parser.Parser();
        
        return (Fact.Fact)visitor.Visit(ast);
    }

    public static Call LoadCall(string code)
    {
        var parser = makeParser(code);
        var ast = parser.call();
        var visitor = new Parser.Parser();
        
        return (Call)visitor.Visit(ast);
    }

    //TODO this probably belongs in Program
    public static Program Merge(Program program, string code)
    {
        var scope = program.Scope;
        var other = load(code).Scope;
        scope.Merge(other);
        var replacement = new Program(scope);
        Linker.LinkAll(scope);
        return replacement;
    }
}