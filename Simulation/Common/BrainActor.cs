using Akka.Actor;
using Fictoria.Simulation.Human.Messages;

namespace Fictoria.Simulation.Common;

public abstract class BrainActor : FictoriaActor
{
    protected void SubscribeToKnowledge()
    {
        // TODO this won't do
        GetActor("..").Tell(new KnowledgeSubscribe());
    }
}