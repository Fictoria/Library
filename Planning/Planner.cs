using Fictoria.Logic;
using Fictoria.Logic.Evaluation;
using Fictoria.Planning.Action;
using Fictoria.Planning.Action.Campfire;
using Fictoria.Planning.Action.Tree;

namespace Fictoria.Planning;

public class Planner
{
    private IList<ActionFactory> _factories;
    
    public Planner(IList<ActionFactory> factories)
    {
        _factories = factories;
    }
    
    public void ForwardSearch(Program program, string goal)
    {
        var queue = new PriorityQueue<Program, int>();
        var visited = new HashSet<Scope>();
        queue.Enqueue(program, 0);
        visited.Add(program.Scope);

        int i = 0;
        
        while (queue.Count > 0)
        {
            i++;
            if (!queue.TryDequeue(out var state, out var priority)) continue;
            if (state.Evaluate(goal) is true)
            {
                Console.WriteLine("goal achieved");
                return;
            }

            foreach (var factory in _factories)
            {
                foreach (var input in factory.Space(state))
                {
                    var action = factory.Create(input);
                    if (state.Evaluate(action.Conditions()) is not true)
                    {
                        continue;
                    }

                    var newState = state.Clone();
                    action.Perform(newState);
                    if (!visited.Contains(newState.Scope))
                    {
                        visited.Add(state.Scope);
                        queue.Enqueue(newState, priority + action.Cost(state));
                    }
                    else
                    {
                        Console.WriteLine("duplicate hit");
                    }
                }
            }
        }
        
        Console.WriteLine("goal failed");
    }
}