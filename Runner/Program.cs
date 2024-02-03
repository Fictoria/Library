using Fictoria.Logic;
using Fictoria.Planning.Planner;
using Fictoria.Planning.Semantic;

namespace Runner;

public static class Program
{
    public static void Main(string[] args)
    {
        var path = "../../../kb/";
        var program = Loader.LoadAll(path);

        var planner = new Planner(program);
        var network = Network.Load("../../../semnet.json");
        if (planner.ForwardSearch(program, network, "warm(self)", out var plan, out var debug))
        {
            Console.WriteLine($"plan: {plan.Steps.Count}");
            Console.WriteLine($"sem:  {debug.Count}");
        }
        // foreach (var a in plan2.Actions)
        // {
        //     Console.WriteLine(a.ToString());
        // }
        // 
        // var output = plan2.RenderToDOT(debug2);
        // File.WriteAllText("../../../debug.dot", output);
        // Console.WriteLine("done");
        // var path = "../../../kb.txt";
        // var text = File.ReadAllText(path);
        //
        // var program = Loader.Load(text);
        // var result = (List<object>)program.Evaluate("&searchable(@all); all");
        // foreach (var x in result)
        // {
        //     Console.WriteLine(x);
        // }

        // Console.WriteLine(result);
        // foreach (var r in results)
        // {
        //     Console.WriteLine(r);
        //     // var result = r as List<object>;
        //     // foreach (var x in result)
        //     // {
        //     //     Console.WriteLine(x);
        //     // }
        // }
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