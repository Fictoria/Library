using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Fact;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Parser;

public class Builder
{
    public static Scope FromStatements(IEnumerable<object> statements)
    {
        var scope = new Scope();
        
        foreach (var type in Type.Type.BuiltIns)
        {
            scope.Types[type.Name] = type;
        }
        
        foreach (var statement in statements)
        {
            switch (statement)
            {
                case Type.Type type:
                    scope.DefineType(type);
                    break;
                case Instance instance:
                    var name = instance.NameExpression.Text;
                    instance.Name = name;
                    instance.Type = new TypePlaceholder(instance.TypeExpression.Text);
                    scope.DefineInstance(instance);
                    break;
                case Schema schema:
                    scope.DefineSchema(schema);
                    break;
                case Fact.Fact fact:
                    if (fact.Schema.Name == "instance")
                    {
                        continue;
                    }
                    
                    scope.DefineFact(fact);
                    break;
                case Antifact antifact:
                    scope.DefineAntifact(antifact);
                    break;
                case Function.Function function:
                    scope.DefineFunction(function);
                    break;
                case Action.Action action:
                    scope.DefineAction(action);
                    break;
            }
        }

        return scope;
    }
}