using Akka.Actor;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Nature.Messages;

namespace Fictoria.Simulation.Nature;

public class Reality : FictoriaActor
{
    private readonly Dictionary<string, IActorRef> _actors = new();

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Exists exists:
                _actors[exists.Id] = Sender;
                break;
            case ActorRequest ra:
                if (_actors.TryGetValue(ra.Id, out var found))
                {
                    Sender.Tell(new ActorReceipt(found));
                }
                else
                {
                    Sender.Tell(new ActorReceipt(null));
                }
                break;
        }
    }
}