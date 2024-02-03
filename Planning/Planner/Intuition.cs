using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Expression;
using Fictoria.Planning.Semantic;
using Action = Fictoria.Logic.Action.Action;
using Type = Fictoria.Logic.Type.Type;

namespace Fictoria.Planning.Planner;

public static class Intuition
{
    public static double Similarity(Expression goal, Action action, Network semantics, Context context)
    {
        var builtins = new HashSet<string>(Type.BuiltIns.Select(t => t.Name));
        var total = 0.0;
        var count = 0.0;

        foreach (var gt in goal.Terms())
        {
            if (builtins.Contains(gt))
            {
                continue;
            }

            foreach (var at in (List<object>)action.Terms.Evaluate(context))
            {
                if (builtins.Contains(at))
                {
                    continue;
                }

                if (semantics.TryGet(gt, (string)at, out var weight))
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

        if (count <= 0.0)
        {
            return 0.05;
        }

        return total / count;
    }
}