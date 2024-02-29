namespace Fictoria.Simulation.Nature.Messages;

public class ActorRequest
{
    public string Id { get; }

    public ActorRequest(string id)
    {
        Id = id;
    }
}