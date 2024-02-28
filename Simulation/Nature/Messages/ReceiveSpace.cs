using Fictoria.Domain.Locality;

namespace Fictoria.Simulation.Nature.Messages;

public class ReceiveSpace
{
    public IReadOnlyList<Location> Results { get; }

    public ReceiveSpace(IReadOnlyList<Location> results)
    {
        Results = results;
    }
}