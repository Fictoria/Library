using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Fact;
using Fictoria.Logic.Type;
using Tuple = Fictoria.Logic.Expression.Tuple;

namespace Fictoria.Logic.Parser;

public class Linker
{
    public static void LinkAll(Program program)
    {
        LinkTypes(program);
        LinkSchemata(program);
        LinkFacts(program);
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
                if (parent.GetType() != typeof(TypePlaceholder)) return parent;
                
                var placeholder = (TypePlaceholder)parent;
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
                if (parameter.Type.GetType() != typeof(TypePlaceholder)) continue;
                
                var placeholder = (TypePlaceholder)parameter.Type;
                if (scope.Types.TryGetValue(placeholder.Name, out var found))
                {
                    parameter.Type = found;
                    continue;
                }

                throw new ParseException($"unknown schema parameter type '{placeholder.Name}'");
            }
        }
    }

    private static void LinkFacts(Program program)
    {
        var scope = program.Scope;
        foreach (var (_, facts) in scope.Facts)
        {
            foreach (var fact in facts)
            {
                if (fact.Schema.GetType() != typeof(SchemaPlaceholder)) continue;
            
                if (scope.Schemata.TryGetValue(fact.Schema.Name, out var found))
                {
                    fact.Schema = found;
                    continue;
                }

                throw new ParseException($"unknown schema '{fact.Schema.Name}'");
            }
        }
    }

    private static void LinkInstances(Program program)
    {
        var scope = program.Scope;
        foreach (var (name, type) in scope.Instances)
        {
            if (type.GetType() != typeof(TypePlaceholder)) continue;
            
            var placeholder = (TypePlaceholder)type;
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
                if (parameter.Type.GetType() != typeof(TypePlaceholder)) continue;
                
                var placeholder = (TypePlaceholder)parameter.Type;
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
            case Wildcard _:
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

                if (scope.Types.TryGetValue(identifier.Name, out var type))
                {
                    expression.Type = type;
                    break;
                }

                throw new ResolveException($"unknown symbol '{identifier.Name}'");
            case Binding binding:
                if (scope.Bindings.TryGetValue("$", out var found))
                {
                    binding.Type = (Type.Type)found;
                    scope.Bindings[binding.Name] = (Type.Type)found;
                    break;
                }
                
                throw new ResolveException($"search binding '{binding}' is not valid here");
            case Parenthetical parenthetical:
                LinkExpression(program, parenthetical);
                expression.Type = parenthetical.Type;
                if (parenthetical.Expression.ContainsBinding)
                {
                    parenthetical.ContainsBinding = true;
                    parenthetical.BindingName = parenthetical.Expression.BindingName;
                }
                break;
            case Tuple tuple:
                foreach (var e in tuple.Expressions)
                {
                    LinkExpression(program, e);
                    if (e.ContainsBinding)
                    {
                        tuple.ContainsBinding = true;
                        tuple.BindingName = e.BindingName;
                    }
                }
                tuple.Type = Type.Type.Tuple;
                break;
            case Call call:
                if (scope.Schemata.TryGetValue(call.Functor, out var schema))
                {
                    expression.Type = Fictoria.Logic.Type.Type.Boolean;

                    var arguments = call.Arguments.ToList();
                    for (int i = 0; i < arguments.Count; i++)
                    {
                        //TODO this loop is inelegant
                        var e = arguments[i];
                        if (e is Binding b)
                        {
                            var parameterType = schema.Parameters[i].Type;
                            scope.Bindings[b.Name] = parameterType;
                            b.Type = parameterType;
                        }
                        
                        scope.Bindings["$"] = schema.Parameters[i].Type;
                        LinkExpression(program, e);
                        scope.Bindings.Remove("$");
                    }
                    break;
                }

                if (scope.Functions.TryGetValue(call.Functor, out var function))
                {
                    foreach (var e in call.Arguments)
                    {
                        LinkExpression(program, e);
                    }
                    
                    expression.Type = function.Expression.Type;
                    break;
                }
                
                throw new ResolveException($"unknown functor '{call.Functor}'");
            case Unary unary:
                LinkExpression(program, unary.Expression);
                if (unary.Expression.ContainsBinding)
                {
                    unary.ContainsBinding = true;
                    unary.BindingName = unary.Expression.BindingName;
                }

                switch (unary.Operator)
                {
                    case "-":
                        if (unary.Expression.Type.Equals(Fictoria.Logic.Type.Type.Int) ||
                            unary.Expression.Type.Equals(Fictoria.Logic.Type.Type.Float))
                        {
                            expression.Type = unary.Expression.Type;
                            break;
                        }
                        throw new ParseException($"invalid type '{unary.Expression.Type}' for unary operator '{unary.Operator}'");
                    case "!":
                        if (unary.Expression.Type.Equals(Fictoria.Logic.Type.Type.Boolean))
                        {
                            expression.Type = Fictoria.Logic.Type.Type.Boolean;
                            break;
                        }
                        throw new ParseException($"invalid type '{unary.Expression.Type}' for unary operator '{unary.Operator}'");
                    default:
                        throw new ParseException($"unknown unary operator '{unary.Operator}'");
                }
                
                break;
            case Infix infix:
                LinkExpression(program, infix.Left);
                LinkExpression(program, infix.Right);
                //TODO this could be wonky
                if (infix.Left.ContainsBinding)
                {
                    infix.ContainsBinding = true;
                    infix.BindingName = infix.Left.BindingName;
                }
                if (infix.Right.ContainsBinding)
                {
                    infix.ContainsBinding = true;
                    infix.BindingName = infix.Right.BindingName;
                }
                
                if (!infix.Left.Type.Equals(infix.Right.Type))
                {
                    throw new ParseException($"mismatched types '{infix.Left.Type}' and '{infix.Right.Type}' for '{infix.Operator}' infix expression");
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
                        throw new ParseException($"unknown infix operator '{infix.Operator}'");
                }
                break;
            case Series series:
                foreach (var e in series.Expressions)
                {
                    LinkExpression(program, e);
                }

                series.Type = series.Expressions.Last().Type;
                break;
            default:
                throw new ParseException($"unknown expression '{expression}'");
        }
    }
}