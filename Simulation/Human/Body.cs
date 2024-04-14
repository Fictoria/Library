using Akka.Actor;
using Fictoria.Domain.Locality;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Human.Messages;
using Fictoria.Simulation.Nature.Messages;

namespace Fictoria.Simulation.Human;

public class Body : FictoriaActor
{
    private readonly double _speed = 0.5;
    private IActorRef _human;
    private Point _position;
    private IActorRef _space;
    private Point _target;
    private bool _walking;

    protected override void PreStart()
    {
        _human = GetActor("..");
        _space = GetSpace();
        _position = new Point(0, 0);
        SubscribeToTime();
    }

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Walk walk:
                _walking = true;
                _target = walk.Point;
                break;
            case Stop:
                _walking = false;
                break;
            case Tick:
                if (!_walking)
                {
                    return;
                }
                var direction = _position.DirectionTo(_target);
                var deltaX = _speed * Math.Cos(direction);
                var deltaY = _speed * Math.Sin(direction);
                var delta = new Point(deltaX, deltaY);
                _position = _position.Add(delta);
                var move = new Move("adam", _position);
                _space.Tell(move);
                _human.Tell(move);
                break;
        }
    }
}