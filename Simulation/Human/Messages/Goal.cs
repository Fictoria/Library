namespace Fictoria.Simulation.Human.Messages;

public class Goal
{
    public string Predicate { get; }

    public Goal(string predicate)
    {
        Predicate = predicate;
    }
}