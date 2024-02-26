namespace Fictoria.Simulation.Nature.Messages;

public class Appear
{
    public string Id { get; }
    public double X { get; }
    public double Y { get; }

    public Appear(string id, double x, double y)
    {
        Id = id;
        X = x;
        Y = y;
    }
}