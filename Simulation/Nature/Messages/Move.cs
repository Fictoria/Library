namespace Fictoria.Simulation.Nature.Messages;

public class Move
{
    public string Id { get; }
    public double X { get; }
    public double Y { get; }

    public Move(string id, double x, double y)
    {
        Id = id;
        X = x;
        Y = y;
    }
}