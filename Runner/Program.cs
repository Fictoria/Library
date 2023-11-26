using Fictoria.Logic;
using Fictoria.Logic.Evaluation;
using Fictoria.Planning;
using Fictoria.Planning.Action;
using Fictoria.Planning.Action.Campfire;
using Fictoria.Planning.Action.General;
using Fictoria.Planning.Action.Homeostasis;
using Fictoria.Planning.Action.Material;
using Fictoria.Planning.Action.Tree;

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
        // var result = program.Evaluate("!warm(self)");
        //
        // var sf = SearchFactory.Instance.Space(program).First();
        // var sa = SearchFactory.Instance.Create(sf);
        // Console.WriteLine(program.Evaluate(sa.Conditions()));
        // var sp = program.Clone();
        // sa.Perform(sp);
        //
        // var cf = ChopFactory.Instance.Space(sp).First();
        // var ca = ChopFactory.Instance.Create(cf);
        // Console.WriteLine(sp.Evaluate(ca.Conditions()));
        // var cp = sp.Clone();
        // ca.Perform(cp);
        //
        // var gf = GatherFactory.Instance.Space(cp).First();
        // var ga = GatherFactory.Instance.Create(gf);
        // Console.WriteLine(cp.Evaluate(ga.Conditions()));
        // var gp = cp.Clone();
        // ga.Perform(gp);
        //
        // var df = DepositFactory.Instance.Space(gp).First();
        // var da = DepositFactory.Instance.Create(df);
        // Console.WriteLine(gp.Evaluate(da.Conditions()));
        // var dp = gp.Clone();
        // da.Perform(dp);
        //
        // var lf = LightFactory.Instance.Space(dp).First();
        // var la = LightFactory.Instance.Create(lf);
        // Console.WriteLine(dp.Evaluate(la.Conditions()));
        // var lp = dp.Clone();
        // la.Perform(lp);
        //
        // var wf = WarmFactory.Instance.Space(lp).First();
        // var wa = WarmFactory.Instance.Create(wf);
        // Console.WriteLine(lp.Evaluate(wa.Conditions()));
        // var wp = lp.Clone();
        // wa.Perform(wp);
        
        // var visited = new HashSet<Scope>();
        // visited.Add(program.Scope);
        // var x = program.Clone();
        // Console.WriteLine(visited.Contains(wp.Scope));
        
        Console.WriteLine();
    }
}