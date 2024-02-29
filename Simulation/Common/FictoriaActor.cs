using Akka.Actor;
using Akka.Event;
using Fictoria.Simulation.Nature.Messages;

namespace Fictoria.Simulation.Common;

public abstract class FictoriaActor : UntypedActor
{
    protected readonly ILoggingAdapter _log = Context.GetLogger();

    protected void SubscribeToTime()
    {
        Context.ActorSelection("/user/nature/time").Tell(new Subscribe());
    }

    protected void Exists(string id)
    {
        GetReality().Tell(new Exists(id));
    }

    protected IActorRef GetReality()
    {
        return GetActor("/user/nature/reality");
    }

    protected IActorRef GetSpace()
    {
        return GetActor("/user/nature/space");
    }

    protected IActorRef GetActor(string path)
    {
        return Context.ActorSelection(path).ResolveOne(TimeSpan.FromSeconds(1)).Result;
    }
}