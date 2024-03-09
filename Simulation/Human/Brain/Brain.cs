using Akka.Actor;
using Fictoria.Logic;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Human.Messages;
using Fictoria.Simulation.Nature.Messages;

namespace Fictoria.Simulation.Human.Brain;

public class Brain : FictoriaActor
{
    private readonly Logic.Program _knowledge = Loader.LoadAll("../../../_kb");
    private readonly List<IActorRef> _subscribers = new();
    private IActorRef? _prefrontalCortex;

    protected override void PreStart()
    {
        SubscribeToTime();
        _prefrontalCortex = Context.ActorOf<PrefrontalCortex>("prefrontal_cortex");
        _prefrontalCortex.Tell(new Goal("warm(self)"));
    }

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Tick:
                var receipt = new KnowledgeReceipt(_knowledge);
                foreach (var subscriber in _subscribers)
                {
                    subscriber.Tell(receipt);
                }
                break;
            case Move move:
                _prefrontalCortex.Tell(move);
                break;
            case KnowledgeSubscribe:
                _subscribers.Add(Sender);
                break;
        }
    }
}