using System.Diagnostics;
using Fictoria.Logic;
using Fictoria.Logic.Evaluation;
using Fictoria.Planning;
using Fictoria.Planning.Action;
using Fictoria.Planning.Action.Campfire;
using Fictoria.Planning.Action.General;
using Fictoria.Planning.Action.Homeostasis;
using Fictoria.Planning.Action.Material;
using Fictoria.Planning.Action.Resource;
using Fictoria.Planning.Planner;
using Fictoria.Planning.Semantic;

namespace Runner;

public static class Program
{
    public static void Main(string[] args)
    {
        var path = "../../../kb.txt";
        var text = File.ReadAllText(path);

        var program = Loader.Load(text);
        var result = program.Evaluate("fib(10)");
        Console.WriteLine(result);
        // var planner = new Planner(new ActionFactory[]
        // {
        //     SearchFactory.Instance,
        //     ExtractFactory.Instance,
        //     GatherFactory.Instance,
        //     DepositFactory.Instance,
        //     LightFactory.Instance,
        //     WarmFactory.Instance
        // });
        //
        // // var network = Analyzer.Analyze(program);
        // // network.Save("../../../semnet.json");
        // var network = Network.Load("../../../semnet.json");
        // if (planner.ForwardSearch(program, "warm(self)", out var plan1, out var debug1))
        // {
        //     Console.WriteLine($"raw: {debug1.Count}");
        // }
        // if (planner.ForwardSearch(program, network, "warm(self)", out var plan2, out var debug2))
        // {
        //     Console.WriteLine($"sem: {debug2.Count}");
        //     // foreach (var a in plan2.Actions)
        //     // {
        //     //     Console.WriteLine(a.ToString());
        //     // }
        //     //
        //     // var output = plan2.RenderToDOT(debug2);
        //     // File.WriteAllText("../../../debug.dot", output);
        //     // Console.WriteLine("done");
        // }
        // var network = Analyzer.Analyze(program);
        // var output = network.RenderToDOT();
        // File.WriteAllText("../../../semnet.dot", output);
        // // Console.WriteLine(Directory.GetCurrentDirectory());
        // network.Save("../../../semnet.json");
    }
}