using Antlr4.Runtime;
using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Lexer;
using Fictoria.Logic.Parser;

namespace Fictoria.Logic;

public class Loader
{
    public static Program Load(string code)
    {
        var program = load(code);
        Linker.LinkAll(program);
        return program;
    }
    
    private static Program load(string code)
    {
        var inputStream = new AntlrInputStream(code);
        var lexer = new LogicLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new LogicParser(tokens);

        var ast = parser.logic();
        var visitor = new Parser.Parser();
        
        return (Program)visitor.Visit(ast);
    }

    public static Expression.Expression LoadExpression(string code)
    {
        var inputStream = new AntlrInputStream(code);
        var lexer = new LogicLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new LogicParser(tokens);

        var ast = parser.expression();
        var visitor = new Parser.Parser();
        
        return (Expression.Expression)visitor.Visit(ast);
    }

    public static Fact.Fact LoadFact(string code)
    {
        var inputStream = new AntlrInputStream(code);
        var lexer = new LogicLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new LogicParser(tokens);

        var ast = parser.fact();
        var visitor = new Parser.Parser();
        
        return (Fact.Fact)visitor.Visit(ast);
    }

    public static Program Merge(Program program, string code)
    {
        var scope = program.Scope;
        var other = Load(code).Scope;
        scope.Merge(other);
        var replacement = new Program(scope);
        Linker.LinkAll(program);
        return replacement;
    }
}