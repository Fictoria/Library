using Akka.Actor;
using Fictoria.Logic;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Human.Messages;

namespace Fictoria.Simulation.Human.Brain;

public class Brain : FictoriaActor
{
    private readonly Logic.Program _knowledge = Loader.LoadAll("../../../_kb");
    private IActorRef? _prefrontalCortex;

    protected override void PreStart()
    {
        _prefrontalCortex = Context.ActorOf<PrefrontalCortex>("prefrontal_cortex");
        _prefrontalCortex.Tell(new Goal("warm(self)"));
    }

    protected override void OnReceive(object message)
    {
        // var goal = message;
        // var achieved = _knowledge.Evaluate((string)goal);
        switch (message)
        {
            case RequestKnowledge:
                Sender.Tell(new ReceiveKnowledge(_knowledge));
                break;
        }
    }
}