using Akka.Actor;

namespace Fictoria.Simulation.Nature.Messages;

public class ActorReceipt
{
    public IActorRef? Actor { get; }

    public ActorReceipt(IActorRef? actor)
    {
        Actor = actor;
    }
}