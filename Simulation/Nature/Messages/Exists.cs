namespace Fictoria.Simulation.Nature.Messages;

public class Exists
{
    public string Id { get; }

    public Exists(string id)
    {
        Id = id;
    }
}