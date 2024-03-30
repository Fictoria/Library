using Akka.Actor;
using Akka.Event;
using Fictoria.Domain.Locality;
using Fictoria.Planning.Planner;
using Fictoria.Planning.Semantic;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Human.Messages;
using Fictoria.Simulation.Nature.Messages;
using Action = Fictoria.Logic.Action.Action;

namespace Fictoria.Simulation.Human.Brain;

// TODO decompose the planning pieces of this into the dorsolateral prefrontal cortex
public class PrefrontalCortex : BrainActor
{
    private readonly Network _network = Network.LoadFromFile("../../../_data/semnet.json");
    private IActorRef? _body;
    private Location? _destination;
    private string? _goal;
    private Logic.Program? _knowledge;
    private Plan? _plan;
    private Planner? _planner;
    private Point? _position;
    private IActorRef? _reality;

    private State _state = State.Idle;
    private int _step = -1;
    private IActorRef? _target;

    protected override void PreStart()
    {
        _reality = GetReality();
        _body = GetActor("../../body");
        SubscribeToTime();
        SubscribeToKnowledge();
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
                break;
            case KnowledgeReceipt rk:
                _knowledge = rk.Knowledge;
                break;
            case ActorReceipt ar:
                _target = ar.Actor;
                break;
            case CompleteAction:
                _step++;
                _state = State.Ready;
                _target = null;
                break;
            case Tick:
                tick();
                break;
        }
    }

    private void tick()
    {
        switch (_state)
        {
            case State.Idle:
                if (_knowledge is not null && _goal is not null && _plan is null)
                {
                    _state = State.Planning;
                    if (planForGoal())
                    {
                        _state = State.Ready;
                        _log.Info($"plan found with {_plan.Steps.Count} steps");
                        _step = 0;
                    }
                    else
                    {
                        _state = State.Idle;
                        _log.Info("plan not found");
                    }
                }
                break;
            case State.Ready:
                var step = _plan.Steps[_step];
                var action = step.Action;
                var location = getTarget(action);
                _destination = location;
                _body.Tell(new Walk(_destination.Point));
                // TODO this should be a helper/utility function somewhere
                var bindings = string.Join(", ",
                    step.Bindings.Take(step.Action.Parameters.Count).Select(p => p.ToString()));
                _log.Info($"action {step.Action.Name}({bindings})");
                _state = State.Walking;
                break;
            case State.Walking:
                var distance = _position?.DistanceTo(_destination.Point);
                if (distance is not null)
                {
                    _log.Info($"walking, distance {distance}");
                }
                if (distance < 0.5)
                {
                    _body.Tell(new Stop());
                    _reality.Tell(new ActorRequest(_destination.Id));
                    _state = State.Acting;
                    _log.Info($"arrived ({_position.X}, {_position.Y})");
                    _destination = null;
                }
                break;
            case State.Acting:
                _target.Tell(new PerformAction(_plan.Steps[_step].Action.Name));
                break;
        }
    }

    private Location getTarget(Action action)
    {
        var target = (Dictionary<string, object>)_knowledge.Evaluate(action.Target);
        var id = target["id"].ToString();
        var x = (double)((List<object>)target["point"])[0];
        var y = (double)((List<object>)target["point"])[1];
        var distance = (double)target["distance"];
        _log.Info($"destination to ({id}) at ({x}, {y}) from distance ({distance})");
        return new Location(id, new Point(x, y));
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

    private enum State
    {
        Idle,
        Planning,
        Ready,
        Walking,
        Acting
    }
}