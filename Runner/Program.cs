using Antlr4.Runtime;
using Fictoria.Logic;
using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Lexer;
using Fictoria.Logic.Parser;
using Parser = Fictoria.Logic.Parser.Parser;

namespace Runner;

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
                   //location(wood1, 5, 2).
                   location(wood2, 3, 4).
                   
                   near(t: thing).
                   carrying(t: thing).
                   
                   can_pick_up(t: thing) = near(t) and !carrying(_).
                   
                   near(wood1).
                   
                   can_find(t: thing) = location(t, _, _).
                   //provides_wood(t: thing) = provides(t, wood).
                   """;

        var program = Loader.Load(text);
        var result = program.Evaluate("can_find(wood)");
        Console.Out.WriteLine(result);
    }
}