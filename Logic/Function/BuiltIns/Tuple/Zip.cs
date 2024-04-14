using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Tuple;

public class Zip : BuiltIn
{
    public string Name => "zip";
    public Type.Type Type => Logic.Type.Type.Tuple;
    public IList<Parameter> Parameters => new List<Parameter>();

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        if (arguments.Count < 2)
        {
            throw new EvaluateException($"built-in function `{Name}` takes 2 or more arguments");
        }

        var tuples = arguments.Select(a => (List<object>)a.Evaluate(context)).ToList();
        // TODO ensure all tuples are of equal length
        var results = new List<object>();

        for (var i = 0; i < tuples[0].Count; i++)
        {
            var row = new List<object>();
            foreach (var tuple in tuples)
            {
                row.Add(tuple[i]);
            }
            results.Add(row);
        }

        return results;
    }
}