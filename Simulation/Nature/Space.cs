using Fictoria.Domain.Locality;
using Fictoria.Simulation.Common;
using Fictoria.Simulation.Nature.Messages;

namespace Fictoria.Simulation.Nature;

public class Space : FictoriaActor
{
    private readonly Constellation _constellation = new();

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Appear appear:
                _constellation.Insert(appear.Id, appear.X, appear.Y);
                break;
            case Disappear disappear:
                _constellation.Remove(disappear.Id);
                break;
            case Move move:
                _constellation.Update(move.Id, move.X, move.Y);
                break;
        }
    }
}