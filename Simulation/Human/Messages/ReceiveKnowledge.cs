namespace Fictoria.Simulation.Human.Messages;

public class ReceiveKnowledge
{
    public Logic.Program Knowledge { get; }

    public ReceiveKnowledge(Logic.Program knowledge)
    {
        Knowledge = knowledge;
    }
}