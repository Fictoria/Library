namespace Fictoria.Simulation.Human.Messages;

public class KnowledgeReceipt
{
    public Logic.Program Knowledge { get; }

    public KnowledgeReceipt(Logic.Program knowledge)
    {
        Knowledge = knowledge;
    }
}