using Fictoria.Logic;

namespace Fictoria.Planning.Action;

public interface ActionFactory
{
    public Action Create(object input);
    public IEnumerable<object> Space(Program program);
}