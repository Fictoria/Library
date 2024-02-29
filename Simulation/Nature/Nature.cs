using Akka.Actor;
using Fictoria.Simulation.Common;

namespace Fictoria.Simulation.Nature;

public class Nature : FictoriaActor
{
    protected override void PreStart()
    {
        Context.ActorOf<Time>("time");
        Context.ActorOf<Space>("space");
        Context.ActorOf<Reality>("reality");
    }

    protected override void OnReceive(object message) { }
}