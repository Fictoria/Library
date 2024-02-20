using Akka.Actor;
using Akka.Event;

namespace Fictoria.Simulation.Common;

public abstract class FictoriaActor : UntypedActor
{
    protected readonly ILoggingAdapter _log = Context.GetLogger();
}