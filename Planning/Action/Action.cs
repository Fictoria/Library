using Fictoria.Logic;

namespace Fictoria.Planning.Action;

public abstract class Action
{
    public abstract int Cost(Program program);
    public abstract string Conditions();
    public abstract string Effects();

    public bool Possible(Program program)
    {
        return program.Evaluate(Conditions()) is true;
    }

    public Program Perform(Program program)
    {
        return Loader.Merge(program, Effects());
    }
}