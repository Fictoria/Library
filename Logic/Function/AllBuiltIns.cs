using Fictoria.Logic.Function.BuiltIns;
using Fictoria.Logic.Function.BuiltIns.Double;
using Fictoria.Logic.Function.BuiltIns.General;
using Fictoria.Logic.Function.BuiltIns.Tuple;
using Fictoria.Logic.Function.BuiltIns.Types;

namespace Fictoria.Logic.Function;

public class AllBuiltIns
{
    public static IEnumerable<BuiltIn> All => new BuiltIn[]
    {
        new Str(),
        new Subtypes(),
        new Instances(),
        new Id(),
        new Sqrt(),
        new Floor(),
        new Ceil(),
        new Typeof(),
        new Zip()
    };

    public static IDictionary<string, BuiltIn> ByName => All.ToDictionary(b => b.Name, b => b);
}