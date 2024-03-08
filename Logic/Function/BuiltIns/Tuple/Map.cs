using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Tuple;

public class Map : BuiltIn
{
    public string Name => "map";
    public Type.Type Type => Logic.Type.Type.Tuple;
    public IList<Parameter> Parameters => new List<Parameter>();

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        var items = (List<object>)arguments[0].Evaluate(context);
        var func = (Function)arguments[1].Evaluate(context);
        var results = new List<object>();

        foreach (var item in items)
        {
            var result = func.Evaluate(context, new List<object> { item });
            results.Add(result);
        }

        return results;
    }
}