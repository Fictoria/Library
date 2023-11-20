using System.Formats.Asn1;
using Antlr4.Runtime;
using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Fact;
using Fictoria.Logic.Lexer;
using Fictoria.Logic.Parser;
using Parser = Fictoria.Logic.Parser.Parser;

namespace Logic;

public static class Program
{
    public static void Main(string[] args)
    {
        var text = """
                   thing: object.
                   action: object.
                   
                   resource: thing.
                   tree: resource.
                   quarry: resource.
                   
                   material: thing.
                   wood: material.
                   stone: material.
                   
                   building: thing.
                   fire_pit: building.
                   
                   provides(r: resource, m: material).
                   consumes(b: building, m: material).
                   
                   provides(tree, wood).
                   provides(quarry, stone).
                   
                   consumes(fire_pit, wood).
                   
                   instance(tree1, tree).
                   instance(tree2, tree).
                   instance(fp1, fire_pit).
                   instance(wood1, wood).
                   instance(wood2, wood).
                   
                   // personal knowledge
                   location(t: thing, x: int, y: int).
                   location(tree1, 5, 2).
                   location(tree2, 3, 4).
                   location(fp1, 4, 5).
                   location(wood1, 5, 2).
                   location(wood2, 3, 4).
                   
                   near(t: thing).
                   carrying(t: thing).
                   
                   near(wood1).
                   
                   double(x: int) = x * 2.
                   greater(x: int, y: int) = x > y.
                   sample() = greater(3, 2) or greater(2, 3).
                   
                   //carrying(wood2).
                   
                   can_pick_up(t: thing) = near(t) and !carrying(_).
                   """;
        
        var inputStream = new AntlrInputStream(text);
        var lexer = new LogicLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new LogicParser(tokens);

        var ast = parser.logic();
        var visitor = new Parser();
        
        var program = (Fictoria.Logic.Program)visitor.Visit(ast);
        Linker.LinkAll(program);
        var context = new Context(program);
        var p = program.Scope.Functions["can_pick_up"];
        var r = p.Evaluate(context, new List<Expression>
        {
            new Identifier("wood1")
        });
        Console.Out.WriteLine(r);
    }
}