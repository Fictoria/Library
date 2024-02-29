using Akka.Event;
using Fictoria.Simulation.Common;

namespace Fictoria.Simulation.Nature.Flora;

public class Tree : FictoriaActor
{
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
                break;
        }
    }
}