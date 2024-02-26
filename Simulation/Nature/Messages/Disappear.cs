namespace Fictoria.Simulation.Nature.Messages;

public class Disappear
{
    public string Id { get; }

    public Disappear(string id)
    {
        Id = id;
    }
}