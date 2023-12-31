using Fictoria.Logic.Function.BuiltIns;
using Fictoria.Logic.Function.BuiltIns.Double;
using Fictoria.Logic.Function.BuiltIns.Types;

namespace Fictoria.Logic.Function;

public class Builtins
{
    public static IEnumerable<BuiltIn> All => new BuiltIn[]
    {
        new Str(),
        new Sqrt()
    };

    public static IDictionary<string, BuiltIn> ByName => All.ToDictionary(b => b.Name, b => b);
}