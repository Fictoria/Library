using Fictoria.Logic.Expression;
using Fictoria.Planning.Semantic;

namespace Fictoria.Planning.Planner;

public class Intuition
{
    public static double Similarity(Expression goal, Action.Action action, Network semantics)
    {
        var total = 0.0;
        var count = 0.0;
        
        foreach (var gt in goal.Terms())
        {
            foreach (var at in action.Terms())
            {
                if (semantics.TryGet(gt, at, out var weight))
                {
                    total += weight;
                    count += 1.0;
                }
            }
        }

        if (count == 0) return 0.05;
        return total / count;
    }
}