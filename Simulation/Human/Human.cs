using Akka.Actor;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Nature.Messages;

namespace Fictoria.Simulation.Human;

public class Human : FictoriaActor
{
    private IActorRef? _body;
    private IActorRef? _brain;

    protected override void PreStart()
    {
        _brain = Context.ActorOf<Brain.Brain>("brain");
        _body = Context.ActorOf<Body>("body");
        GetSpace().Tell(new Appear("adam", 0, 0));
    }

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Move move:
                _brain.Tell(move);
                break;
        }
    }
}