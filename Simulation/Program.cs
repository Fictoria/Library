using Akka.Actor;

namespace Fictoria.Simulation;

public static class Program
{
    public static void Main(string[] args)
    {
        var system = ActorSystem.Create("fictoria");
        var nature = system.ActorOf<Nature.Nature>("nature");
        var adam = system.ActorOf<Human.Human>("human");

        Console.Read();
    }
}