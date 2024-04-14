using Fictoria.Domain.Locality;

namespace Fictoria.Simulation.Nature.Messages;

public class SpaceReceipt
{
    public IReadOnlyList<Location> Results { get; }

    public SpaceReceipt(IReadOnlyList<Location> results)
    {
        Results = results;
    }
}