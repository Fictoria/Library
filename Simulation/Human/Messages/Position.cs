namespace Fictoria.Simulation.Human.Messages;

public class Position
{
    public double X { get; }
    public double Y { get; }

    public Position(double x, double y)
    {
        X = x;
        Y = y;
    }
}