using Akka.Actor;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Nature.Flora;

namespace Fictoria.Simulation.Nature;

public class Nature : FictoriaActor
{
    protected override void PreStart()
    {
        Context.ActorOf<Time>("time");
        Context.ActorOf<Space>("space");
        Context.ActorOf<Reality>("reality");
        
        Context.ActorOf<Tree>("tree1");
    }

    protected override void OnReceive(object message) { }
}