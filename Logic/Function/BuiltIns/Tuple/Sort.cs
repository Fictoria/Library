using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Function.BuiltIns.Tuple;

public class Sort : BuiltIn
{
    public string Name => "sort";
    public Type.Type Type => Logic.Type.Type.Tuple;
    public IList<Parameter> Parameters => new List<Parameter>();

    public object Evaluate(Context context, IList<Expression.Expression> arguments)
    {
        var tuple = (List<object>)arguments[0].Evaluate(context);
        var func = arguments[1].Evaluate(context);

        switch (func)
        {
            case Function f:
                var mapped = tuple.Select(a => (a, f.Evaluate(context, new List<object> { a })));
                var sorted = mapped.OrderBy(a => a.Item2);
                return sorted.Select(a => a.a).ToList();
        }

        throw new EvaluateException($"`{arguments[1].Text}` is not a function or lambda");
    }
}