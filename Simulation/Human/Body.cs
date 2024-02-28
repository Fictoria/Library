using Akka.Actor;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Human.Messages;
using Fictoria.Simulation.Nature.Messages;

namespace Fictoria.Simulation.Human;

public class Body : FictoriaActor
{
    private readonly double _speed = 0.25;
    private IActorRef _human;
    private IActorRef _space;
    private double _targetX;
    private double _targetY;
    private bool _walking;
    private double _x;
    private double _y;

    protected override void PreStart()
    {
        _human = GetActor("..");
        _space = GetSpace();
        SubscribeToTime();
    }

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Walk walk:
                _walking = true;
                _targetX = walk.X;
                _targetY = walk.Y;
                break;
            case Stop:
                _walking = false;
                break;
            case Tick:
                var direction = Math.Atan2(_targetY - _y, _targetX - _x);
                var deltaX = _speed * Math.Cos(direction);
                var deltaY = _speed * Math.Sin(direction);
                _x += deltaX;
                _y += deltaY;
                var move = new Move("adam", _x, _y);
                _space.Tell(move);
                _human.Tell(move);
                break;
        }
    }
}