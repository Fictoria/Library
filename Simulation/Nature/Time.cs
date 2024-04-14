using Akka.Actor;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Nature.Messages;

namespace Fictoria.Simulation.Nature;

public class Time : FictoriaActor, IWithTimers
{
    private readonly List<IActorRef> _subscribers = new();
    public ITimerScheduler Timers { get; set; }

    protected override void PreStart()
    {
        Timers.StartPeriodicTimer(
            "tick",
            new Tick(),
            TimeSpan.FromSeconds(0),
            TimeSpan.FromSeconds(1)
        );
    }

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Tick:
                foreach (var subscriber in _subscribers)
                {
                    subscriber.Tell(message);
                }
                break;
            case Subscribe:
                _subscribers.Add(Sender);
                break;
        }
    }
}