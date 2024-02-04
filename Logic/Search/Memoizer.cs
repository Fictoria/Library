using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Search;

public class Memoizer
{
    private readonly IDictionary<int, object> _results;

    public Memoizer(IList<Expression.Expression> arguments)
    {
        Arguments = arguments;
        _results = new Dictionary<int, object>();
    }

    public IList<Expression.Expression> Arguments { get; }

    public object Value(Context context, int index)
    {
        if (_results.TryGetValue(index, out var found))
        {
            return found;
        }

        var result = Arguments[index].Evaluate(context);

        if (result is string s && context.ResolveInstance(s, out var found2))
        {
            result = found2;
        }

        if (!Arguments[index].ContainsBinding)
        {
            _results[index] = result;
        }

        return result;
    }
}