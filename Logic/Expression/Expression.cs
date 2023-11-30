using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public abstract class Expression
{
    public string Text { get; }
    public Type.Type Type { get; set; }
    //TODO this passing bindings upward during linking is very leaky
    public bool ContainsBinding { get; set; }
    public string BindingName { get; set; }

    public abstract object Evaluate(Context context);
    public abstract IEnumerable<string> Terms();

    protected Expression(string text, Type.Type? type = null)
    {
        Text = text;
        Type = type ?? Fictoria.Logic.Type.Type.Nothing;
        ContainsBinding = false;
        BindingName = "";
    }
    
    public override string ToString()
    {
        return Text;
    }
}