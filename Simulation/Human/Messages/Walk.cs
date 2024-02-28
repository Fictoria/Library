namespace Fictoria.Simulation.Human.Messages;

public class Walk
{
    public double X { get; }
    public double Y { get; }

    public Walk(double x, double y)
    {
        X = x;
        Y = y;
    }
}