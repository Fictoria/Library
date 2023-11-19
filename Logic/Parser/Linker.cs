using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Parser;

public class Linker
{
    public static void LinkAll(Program program)
    {
        LinkTypes(program);
        LinkSchemata(program);
        LinkInstances(program);
        LinkFunctions(program);
    }
    
    private static void LinkTypes(Program program)
    {
        var scope = program.Scope;
        foreach (var (_, type) in scope.Types)
        {
            type.Parents = type.Parents.Select(parent =>
            {
                if (parent.GetType() != typeof(Placeholder)) return parent;
                
                var placeholder = (Placeholder)parent;
                if (scope.Types.TryGetValue(placeholder.Name, out var found))
                {
                    return found;
                }

                throw new ParseException($"unknown parent type '{placeholder.Name}'");
            }).ToList();
        }
    }

    private static void LinkSchemata(Program program)
    {
        var scope = program.Scope;
        foreach (var (_, schema) in scope.Schemata)
        {
            foreach (var parameter in schema.Parameters)
            {
                if (parameter.Type.GetType() != typeof(Placeholder)) continue;
                
                var placeholder = (Placeholder)parameter.Type;
                if (scope.Types.TryGetValue(placeholder.Name, out var found))
                {
                    parameter.Type = found;
                    continue;
                }

                throw new ParseException($"unknown schema parameter type '{placeholder.Name}'");
            }
        }
    }

    private static void LinkInstances(Program program)
    {
        var scope = program.Scope;
        foreach (var (name, type) in scope.Instances)
        {
            if (type.GetType() != typeof(Placeholder)) continue;
            
            var placeholder = (Placeholder)type;
            if (scope.Types.TryGetValue(placeholder.Name, out var found))
            {
                scope.Instances[name] = found;
                continue;
            }

            throw new ParseException($"unknown instance type '{placeholder.Name}'");
        }
    }
    
    private static void LinkFunctions(Program program)
    {
        var scope = program.Scope;
        foreach (var (_, function) in scope.Functions)
        {
            foreach (var parameter in function.Parameters)
            {
                if (parameter.Type.GetType() != typeof(Placeholder)) continue;
                
                var placeholder = (Placeholder)parameter.Type;
                if (scope.Types.TryGetValue(placeholder.Name, out var found))
                {
                    parameter.Type = found;
                    scope.Bindings[parameter.Name] = found;
                    continue;
                }

                throw new ParseException($"unknown predicate parameter type '{placeholder.Name}'");
            }
            LinkExpression(program, function.Expression);
        }
    }

    private static void LinkExpression(Program program, Expression.Expression expression)
    {
        var scope = program.Scope;
        switch (expression)
        {
            case Literal _:
                // nop
                break;
            case Identifier identifier:
                if (scope.Bindings.TryGetValue(identifier.Name, out var variable))
                {
                    expression.Type = (Type.Type)variable;
                    break;
                }

                if (scope.Instances.TryGetValue(identifier.Name, out var instance))
                {
                    expression.Type = instance;
                    break;
                }

                throw new ResolveException($"unknown symbol '{identifier.Name}'");
            case Parenthetical parenthetical:
                LinkExpression(program, parenthetical);
                expression.Type = parenthetical.Type;
                break;
            case Call call:
                foreach (var e in call.Arguments)
                {
                    LinkExpression(program, e);
                }

                // TODO this will cause a double-resolve; can solve with finer granularity models
                if (scope.Schemata.TryGetValue(call.Functor, out var _))
                {
                    expression.Type = Fictoria.Logic.Type.Type.Boolean;
                    break;
                }

                if (scope.Functions.TryGetValue(call.Functor, out var function))
                {
                    expression.Type = function.Expression.Type;
                    break;
                }
                
                throw new ResolveException($"unknown functor '{call.Functor}'");
            case Unary unary:
                LinkExpression(program, unary.Expression);
                expression.Type = unary.Expression.Type;
                break;
            case Infix infix:
                LinkExpression(program, infix.Left);
                LinkExpression(program, infix.Right);
                if (!infix.Left.Type.Equals(infix.Right.Type))
                {
                    throw new ParseException($"mismatched types '{infix.Left}' and '{infix.Right}' for '{infix.Operator}' infix expression");
                }

                switch (infix.Operator)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/": // hmm
                        if (infix.Left.Type.Equals(Fictoria.Logic.Type.Type.Int) ||
                            infix.Left.Type.Equals(Fictoria.Logic.Type.Type.Float))
                        {
                            expression.Type = infix.Left.Type;
                            break;
                        }
                        throw new ParseException($"invalid types '{infix.Left}' and '{infix.Right}' for '{infix.Operator}' infix expression");
                    case ">":
                    case "<":
                    case ">=":
                    case "<=":
                    case "==":
                    case "!=":
                        // TODO isA instead of equals?
                        if (infix.Left.Type.Equals(Fictoria.Logic.Type.Type.Int) ||
                            infix.Left.Type.Equals(Fictoria.Logic.Type.Type.Float))
                        {
                            expression.Type = Fictoria.Logic.Type.Type.Boolean;
                            break;
                        }
                        throw new ParseException($"invalid types '{infix.Left}' and '{infix.Right}' for '{infix.Operator}' infix expression");
                    case "and":
                    case "or":
                    case "xor":
                        if (infix.Left.Type.Equals(Fictoria.Logic.Type.Type.Boolean))
                        {
                            expression.Type = Fictoria.Logic.Type.Type.Boolean;
                            break;
                        }
                        throw new ParseException($"invalid types '{infix.Left}' and '{infix.Right}' for '{infix.Operator}' infix expression");
                    default:
                        throw new ParseException($"invalid infix operator '{infix.Operator}'");
                }
                break;
        }
    }
}