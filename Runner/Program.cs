using System.Reflection;
using Fictoria.Logic;
using Fictoria.Logic.Parser;
using Fictoria.Planning.Planner;
using Fictoria.Planning.Semantic;

namespace Fictoria.Runner;

public static class Program
{
    private static Logic.Program program = Loader.Load("repl:object.");

    private static void REPL()
    {
        while (true)
        {
            Console.Write("> ");
            var input = GetUserInput();
            try
            {
                Handle(input!);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private static string GetUserInput()
    {
        var accumulator = "";
        while (true)
        {
            var input = Console.ReadLine();
            accumulator += input;
            var trimmed = accumulator.Trim();
            if (trimmed.EndsWith(".") || trimmed.EndsWith("?"))
            {
                break;
            }
            Console.Write("  ");
        }

        return accumulator;
    }

    private static void Handle(string input)
    {
        switch (input)
        {
            case not null when input.StartsWith(":load "):
                var path = input.Remove(0, 6);
                var loadedProgram = Loader.LoadAll(path);
                program.Merge(loadedProgram.Scope);
                Linker.LinkAll(program.Scope);
                Console.WriteLine("OK");
                break;
            case not null when input.StartsWith(":plan "):
                var goal = input.Remove(0, 6);
                var planner = new Planner(program);
                var network = Network.LoadFromFile("../../../semnet.json");
                if (planner.ForwardSearch(program, network, goal, out var plan, out var debug))
                {
                    Console.WriteLine($"Plan found ({plan.Steps.Count} steps)");
                    for (var i = 0; i < plan.Steps.Count; i++)
                    {
                        var step = plan.Steps[i];
                        var parameters = string.Join(", ", step.Action.Parameters.Select(p => p.ToString()));
                        var bindings = string.Join(", ",
                            step.Bindings.Take(step.Action.Parameters.Count).Select(p => p.ToString()));
                        Console.WriteLine($"  [{i + 1}] {step.Action.Name}({parameters})");
                        Console.WriteLine($"      {bindings}");
                    }
                }
                else
                {
                    Console.WriteLine("Plan not found");
                }

                break;
            case ":reset":
                program = Loader.Load("repl:object.");
                Console.WriteLine("OK");
                break;
            case ":exit":
            case ":quit":
                Console.WriteLine("GOODBYE");
                Environment.Exit(0);
                break;
            case ":help":
                // TODO fill this out
                Console.WriteLine("Type :exit to leave, otherwise enter statements or expressions");
                break;
            case not null when input.StartsWith(":"):
                Console.WriteLine("UNKNOWN");
                break;
            default:
                var inputProgram = Loader.LoadUnlinked(input);
                program.Merge(inputProgram.Scope);
                if (inputProgram.Scope.Queries.Count == 0)
                {
                    Console.WriteLine("OK");
                }
                else
                {
                    foreach (var query in inputProgram.Scope.Queries)
                    {
                        var result = program.Evaluate(query.Expression);
                        Console.WriteLine(PrettyPrint.Format(result));
                    }
                }

                break;
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine($"Welcome to Fictoria Knowledge v{Assembly.GetExecutingAssembly().GetName().Version}");
        Console.WriteLine("Type in statements and expressions for evaluation, or try :help\n");
        REPL();

        // var path = "../../../kb/";
        // var program = Loader.LoadAll(path);
        //
        // var planner = new Planner(program);
        // var network = Network.LoadFromFile("../../../semnet.json");
        // if (planner.ForwardSearch(program, network, "warm(self)", out var plan, out var debug))
        // {
        //     Console.WriteLine($"plan: {plan.Steps.Count}");
        //     Console.WriteLine($"sem:  {debug.Count}");
        // }


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