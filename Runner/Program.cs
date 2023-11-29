using Fictoria.Logic;
using Fictoria.Logic.Evaluation;
using Fictoria.Planning;
using Fictoria.Planning.Action;
using Fictoria.Planning.Action.Campfire;
using Fictoria.Planning.Action.General;
using Fictoria.Planning.Action.Homeostasis;
using Fictoria.Planning.Action.Material;
using Fictoria.Planning.Action.Tree;
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
            ChopFactory.Instance,
            GatherFactory.Instance,
            DepositFactory.Instance,
            LightFactory.Instance,
            WarmFactory.Instance
        });
        planner.ForwardSearch(program, "warm(self)");
        // var network = Analyzer.Analyze(program);
        // var network = Network.Load("../../../semnet.json");
        // var output = network.RenderToDOT();
        // File.WriteAllText("../../../semnet.dot", output);
        // Console.WriteLine(Directory.GetCurrentDirectory());
        // network.Save("../../../semnet.json");
    }
}