using Fictoria.Logic.Expression;
using Fictoria.Planning.Semantic;
using Type = Fictoria.Logic.Type.Type;

namespace Fictoria.Planning.Planner;

public class Intuition
{
    public static double Similarity(Expression goal, Action.Action action, Network semantics)
    {
        var builtins = new HashSet<string>(Type.BuiltIns.Select(t => t.Name));
        var total = 0.0;
        var count = 0.0;
        
        foreach (var gt in goal.Terms())
        {
            if (builtins.Contains(gt)) continue;   
            foreach (var at in action.Terms())
            {
                if (builtins.Contains(at)) continue;
                if (semantics.TryGet(gt, at, out var weight))
                {
                    total += weight;
                    count += 1.0;
                }
                else
                {
                    count -= 0.05;
                }
            }
        }

        if (count <= 0.0) return 0.05;
        return total / count;
    }
}