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
        var planner = new Planner(new ActionFactory[]
        {
            SearchFactory.Instance,
            ExtractFactory.Instance,
            GatherFactory.Instance,
            DepositFactory.Instance,
            LightFactory.Instance,
            WarmFactory.Instance
        });
        if (planner.ForwardSearch(program, "warm(self)", out var plan, out var debug))
        {
            foreach (var a in plan.Actions)
            {
                Console.WriteLine(a.ToString());
            }
        
            var output = plan.RenderToDOT(debug);
            File.WriteAllText("../../../debug.dot", output);
            Console.WriteLine("done");
        }
        // var network = Analyzer.Analyze(program);
        // // var network = Network.Load("../../../semnet.json");
        // var output = network.RenderToDOT();
        // File.WriteAllText("../../../semnet.dot", output);
        // // Console.WriteLine(Directory.GetCurrentDirectory());
        // network.Save("../../../semnet.json");
    }
}