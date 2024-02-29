using Akka.Actor;
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
                _constellation.Insert(appear.Id, appear.Point.X, appear.Point.Y);
                break;
            case Disappear disappear:
                _constellation.Remove(disappear.Id);
                break;
            case Move move:
                _constellation.Update(move.Id, move.Point.X, move.Point.Y);
                break;
            case SpaceRequest request:
                var results = _constellation.Search(request.Point, request.Distance);
                var response = new SpaceReceipt(results);
                Sender.Tell(response);
                break;
        }
    }
}