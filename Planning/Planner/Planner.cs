using Fictoria.Logic;
using Fictoria.Logic.Evaluation;
using Fictoria.Planning.Semantic;
using Action = Fictoria.Logic.Action.Action;

namespace Fictoria.Planning.Planner;

public class Planner
{
    private readonly IList<Action> _actions;

    public Planner(Program factories)
    {
        _actions = factories.Scope.Actions.Values.ToList();
    }

    // public bool ForwardSearch(Program program, string goal, out Plan plan, out IList<Plan> debug)
    // {
    //     var _debug = new List<Plan>();
    //     debug = _debug;
    //
    //     var queue = new PriorityQueue<Plan, int>();
    //     var visited = new HashSet<Scope>();
    //     var initial = new Plan(program);
    //     queue.Enqueue(initial, 0);
    //     visited.Add(program.Scope);
    //
    //     var i = 0;
    //
    //     while (queue.Count > 0)
    //     {
    //         i++;
    //         if (!queue.TryDequeue(out var node, out var priority))
    //         {
    //             break;
    //         }
    //
    //         _debug.Add(node);
    //
    //         var state = node.State;
    //         if (state.Evaluate(goal) is true)
    //         {
    //             plan = node;
    //             return true;
    //         }
    //
    //         foreach (var action in _actions)
    //         foreach (var input in (List<object>)state.Evaluate(action.Space))
    //         {
    //             var values = input as List<object> ?? new List<object> { input };
    //             var bindings =
    //                 action.Parameters.Zip(values).ToDictionary(b => b.First.Name, b => b.Second);
    //             var step = new Step(action, bindings);
    //             if (state.Evaluate(action.Conditions, bindings) is not true)
    //             {
    //                 continue;
    //             }
    //
    //             var newState = state.Clone();
    //             newState.Merge(action.Effects, bindings);
    //             if (!visited.Contains(newState.Scope))
    //             {
    //                 var newNode = new Plan(newState, node, step);
    //                 visited.Add(state.Scope);
    //                 queue.Enqueue(newNode, priority + (int)state.Evaluate(action.Cost, bindings));
    //             }
    //         }
    //     }
    //
    //     plan = null;
    //     return false;
    // }

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

        var i = 0;

        while (queue.Count > 0)
        {
            i++;
            if (!queue.TryDequeue(out var node, out var priority))
            {
                break;
            }

            _debug.Add(node);

            var state = node.State;
            if (state.Evaluate(goal) is true)
            {
                plan = node;
                return true;
            }

            foreach (var action in _actions)
            foreach (var input in (List<object>)state.Evaluate(action.Space))
            {
                var values = input as List<object> ?? new List<object> { input };
                var bindings =
                    action.Parameters.Zip(values).ToDictionary(b => b.First.Name, b => b.Second);
                var step = new Step(action, bindings);
                if (state.Evaluate(action.Conditions, bindings) is not true)
                {
                    continue;
                }

                var context = new Context(state);
                state.Evaluate(context, action.Locals, bindings);
                foreach (var (k, v) in context.Stack.Peek().Bindings)
                {
                    // TODO need type and value –
                    //      type by switching on the object's type?
                    //      type by managing another dict of binding types in scope/context?
                    // TODO fix how built-in Str() works (can't ToString types)
                    bindings[k] = v;
                }

                var newState = state.Clone();
                newState.Merge(action.Effects, bindings);
                if (!visited.Contains(newState.Scope))
                {
                    var newNode = new Plan(newState, node, step);
                    visited.Add(state.Scope);
                    var distance = 1.0 - Intuition.Similarity(goalExpression, action, semantics, new Context(newState));
                    var cost = (long)state.Evaluate(action.Cost, bindings) * distance;
                    queue.Enqueue(newNode, priority + cost);
                }
            }
        }

        plan = null;
        return false;
    }
}