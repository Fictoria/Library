using Fictoria.Logic;

namespace Fictoria.Planning.Action;

public interface ActionFactory<out PRODUCES, CONSUMES>
{
    public PRODUCES Create(CONSUMES input);
    public IEnumerable<CONSUMES> Space(Program program);
}