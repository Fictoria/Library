using Akka.Actor;
using Fictoria.Simulation.Common;

namespace Fictoria.Simulation.Human;

public class Human : FictoriaActor
{
    private IActorRef? _brain;

    protected override void PreStart()
    {
        _brain = Context.ActorOf<Brain.Brain>("brain");
    }

    protected override void OnReceive(object message) { }
}