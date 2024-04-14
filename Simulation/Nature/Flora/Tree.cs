using Akka.Actor;
using Akka.Event;
using Fictoria.Simulation.Common;

namespace Fictoria.Simulation.Nature.Flora;

public class Tree : FictoriaActor
{
    private int _capacity = 5;

    protected override void PreStart()
    {
        Exists("tree1");
    }

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case PerformAction pa:
                _log.Info($"performing {pa.Name}");
                _capacity -= 1;
                if (_capacity <= 0)
                {
                    Sender.Tell(new CompleteAction());
                    _log.Info("action complete");
                }
                break;
        }
    }
}