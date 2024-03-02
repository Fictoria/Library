using Akka.Actor;
using Akka.Event;
using Fictoria.Domain.Locality;
using Fictoria.Planning.Planner;
using Fictoria.Planning.Semantic;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Human.Messages;
using Fictoria.Simulation.Nature.Messages;

namespace Fictoria.Simulation.Human.Brain;

// TODO decompose the planning pieces of this into the dorsolateral prefrontal cortex
public class PrefrontalCortex : FictoriaActor
{
    private readonly Network _network = Network.LoadFromFile("../../../_data/semnet.json");
    private IActorRef? _body;
    private IActorRef? _brain;
    private Point? _destination;
    private string? _goal;
    private Logic.Program? _knowledge;
    private Plan? _plan;
    private Planner? _planner;

    private Point? _position;
    private int _step = -1;

    protected override void PreStart()
    {
        _brain = GetActor("..");
        _body = GetActor("../../body");
        SubscribeToTime();
    }

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Move move:
                _position = move.Point;
                break;
            case Goal g:
                _goal = g.Predicate;
                _log.Info($"received goal {_goal}");
                requestKnowledge();
                _log.Info("requesting knowledge");
                break;
            case KnowledgeReceipt rk:
                _knowledge = rk.Knowledge;
                _log.Info("received knowledge");
                if (isGoalAchieved())
                {
                    // done
                    _log.Info("goal already achieved");
                }
                else
                {
                    _log.Info("planning for goal");
                    if (planForGoal())
                    {
                        _log.Info($"plan found with {_plan.Steps.Count} steps");
                        _step = 0;
                    }
                    else
                    {
                        _log.Info("plan not found");
                    }
                }
                break;
            case Tick:
                if (_knowledge is null || _goal is null || _plan is null)
                {
                    return;
                }
                var step = _plan.Steps[_step];
                var action = step.Action;
                if (_destination is null && action.Target is not null)
                {
                    var target = _knowledge.Evaluate(action.Target) as List<object>;
                    var id = (double)target![0];
                    var x = (double)target![1];
                    var y = (double)target![2];
                    var point = new Point(x, y);
                    _destination = point;
                    _body.Tell(new Walk(point));
                    // TODO this should be a helper/utility function somewhere
                    var bindings = string.Join(", ",
                        step.Bindings.Take(step.Action.Parameters.Count).Select(p => p.ToString()));
                    _log.Info($"action {step.Action.Name}({bindings})");
                    _log.Info($"destination ({x}, {y})");
                }
                else if (_destination is not null)
                {
                    var distance = _position?.DistanceTo(_destination);
                    if (distance is not null)
                    {
                        _log.Info($"walking, distance {distance}");
                    }
                    if (distance < 0.5)
                    {
                        _destination = null;
                        _body.Tell(new Stop());
                        _log.Info($"arrived ({_position.X}, {_position.Y})");
                        _step++;
                    }
                }
                break;
        }
    }

    private void requestKnowledge()
    {
        _brain.Tell(new KnowledgeRequest());
    }

    private bool isGoalAchieved()
    {
        if (_goal is null)
        {
            return false;
        }
        return _knowledge?.Evaluate(_goal) is true;
    }

    private bool planForGoal()
    {
        if (_knowledge is null || _goal is null)
        {
            return false;
        }
        // TODO this api is a little weird, the constructor and search methods both take the KB
        _planner = new Planner(_knowledge);
        if (_planner.ForwardSearch(_knowledge, _network, _goal, out var plan, out var debug))
        {
            _plan = plan;
            return true;
        }
        return false;
    }
}