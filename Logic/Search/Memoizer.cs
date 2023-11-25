using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Search;

public class Memoizer
{
    public IList<Expression.Expression> Arguments { get; }
    private IDictionary<int, object> _results;

    public Memoizer(IList<Expression.Expression> arguments)
    {
        Arguments = arguments;
        _results = new Dictionary<int, object>();
    }

    public object Value(Context context, int index)
    {
        if (_results.TryGetValue(index, out var found))
        {
            return found;
        }

        var result = Arguments[index].Evaluate(context);
        
        if (Arguments[index] is not Expression.Search)
        {
            _results[index] = result;
        }

        return result;
    }
}