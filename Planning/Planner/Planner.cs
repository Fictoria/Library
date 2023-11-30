using Fictoria.Logic;
using Fictoria.Logic.Evaluation;
using Fictoria.Planning.Action;
using Fictoria.Planning.Semantic;

namespace Fictoria.Planning.Planner;

public class Planner
{
    private IList<ActionFactory> _factories;
    
    public Planner(IList<ActionFactory> factories)
    {
        _factories = factories;
    }
    
    public bool ForwardSearch(Program program, string goal, out Plan plan, out IList<Plan> debug)
    {
        var _debug = new List<Plan>();
        debug = _debug;
        
        var queue = new PriorityQueue<Plan, int>();
        var visited = new HashSet<Scope>();
        var initial = new Plan(program);
        queue.Enqueue(initial, 0);
        visited.Add(program.Scope);

        int i = 0;
        
        while (queue.Count > 0)
        {
            i++;
            if (!queue.TryDequeue(out var node, out var priority)) break;
            
            _debug.Add(node);
            
            var state = node.State;
            if (state.Evaluate(goal) is true)
            {
                plan = node;
                return true;
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
                        var newNode = new Plan(newState, node, action);
                        visited.Add(state.Scope);
                        queue.Enqueue(newNode, priority + action.Cost(state));
                    }
                    else
                    {
                        //Console.WriteLine("duplicate hit");
                    }
                }
            }
        }
        
        plan = null;
        return false;
    }
    
    public bool ForwardSearch(Program program, Network semantics, string goal, out Plan plan, out IList<Plan> debug)
    {
        var _debug = new List<Plan>();
        debug = _debug;
        
        var queue = new PriorityQueue<Plan, double>();
        var visited = new HashSet<Scope>();
        var initial = new Plan(program);
        var goalExpression = Loader.LoadExpression(goal);
        queue.Enqueue(initial, 0);
        visited.Add(program.Scope);

        int i = 0;
        
        while (queue.Count > 0)
        {
            i++;
            if (!queue.TryDequeue(out var node, out var priority)) break;
            
            _debug.Add(node);
            
            var state = node.State;
            if (state.Evaluate(goal) is true)
            {
                plan = node;
                return true;
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
                        var newNode = new Plan(newState, node, action);
                        visited.Add(state.Scope);
                        var distance = 1.0 - Intuition.Similarity(goalExpression, action, semantics);
                        var cost = action.Cost(state) * distance;
                        queue.Enqueue(newNode, priority + cost);
                    }
                    else
                    {
                        //Console.WriteLine("duplicate hit");
                    }
                }
            }
        }
        
        plan = null;
        return false;
    }
}