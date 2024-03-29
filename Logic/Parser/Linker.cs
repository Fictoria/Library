using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Fact;
using Fictoria.Logic.Function;
using Fictoria.Logic.Type;
using Tuple = Fictoria.Logic.Expression.Tuple;

namespace Fictoria.Logic.Parser;

public class Linker
{
    private static string _currentFunctionName = "";
    private static Function.Function? _currentFunction;

    private static bool _callMany;

    public static void LinkAll(Scope scope)
    {
        LinkTypes(scope);
        LinkSchemata(scope);
        LinkFacts(scope);
        LinkAntifacts(scope);
        LinkInstances(scope);
        LinkFunctions(scope);
    }

    private static void LinkTypes(Scope scope)
    {
        foreach (var (_, type) in scope.Types)
        {
            type.Parents = type.Parents.Select(parent =>
            {
                if (parent.GetType() != typeof(TypePlaceholder))
                {
                    return parent;
                }

                var placeholder = (TypePlaceholder)parent;
                if (scope.Types.TryGetValue(placeholder.Name, out var found))
                {
                    LinkParent(scope, type, found);
                    return found;
                }

                if (scope.Bindings.TryGetValue(placeholder.Name, out var binding))
                {
                    LinkParent(scope, type, (Type.Type)binding);
                    return (Type.Type)binding;
                }

                throw new ParseException($"unknown parent type '{placeholder.Name}'");
            }).ToList();
        }
    }

    private static void LinkParent(Scope scope, Type.Type child, Type.Type parent)
    {
        if (!scope.TypesByParent.ContainsKey(parent))
        {
            scope.TypesByParent[parent] = new HashSet<Type.Type>();
        }

        scope.TypesByParent[parent].Add(child);
    }

    private static void LinkSchemata(Scope scope)
    {
        foreach (var (_, schema) in scope.Schemata)
        foreach (var parameter in schema.Parameters)
        {
            if (parameter.Type.GetType() != typeof(TypePlaceholder))
            {
                continue;
            }

            var placeholder = (TypePlaceholder)parameter.Type;
            if (scope.Types.TryGetValue(placeholder.Name, out var found))
            {
                parameter.Type = found;
                continue;
            }

            if (scope.Bindings.TryGetValue(placeholder.Name, out var binding))
            {
                parameter.Type = (Type.Type)binding;
                continue;
            }

            throw new ParseException($"unknown schema parameter type '{placeholder.Name}'");
        }
    }

    private static void LinkFacts(Scope scope)
    {
        foreach (var (_, facts) in scope.Facts)
        foreach (var fact in facts)
        {
            if (fact.Schema.GetType() != typeof(SchemaPlaceholder))
            {
                continue;
            }

            if (scope.Schemata.TryGetValue(fact.Schema.Name, out var found))
            {
                fact.Schema = found;
                fact.Arguments.ForEach(a => LinkExpression(scope, a));
                continue;
            }
            // TODO allow schema bindings?

            throw new ParseException($"unknown schema '{fact.Schema.Name}'");
        }
    }

    private static void LinkAntifacts(Scope scope)
    {
        foreach (var af in scope.Antifacts)
        {
            LinkExpression(scope, af.Matcher);
        }
    }

    private static void LinkInstances(Scope scope)
    {
        foreach (var (_, instance) in scope.Instances)
        {
            var type = instance.Type;
            if (type.GetType() != typeof(TypePlaceholder))
            {
                continue;
            }

            var placeholder = (TypePlaceholder)type;
            if (scope.Types.TryGetValue(placeholder.Name, out var found))
            {
                instance.Type = found;
                continue;
            }

            if (scope.Bindings.TryGetValue(placeholder.Name, out var binding))
            {
                instance.Type = (Type.Type)binding;
                continue;
            }

            throw new ParseException($"unknown instance type '{placeholder.Name}'");
        }
    }

    private static void LinkFunctions(Scope scope)
    {
        foreach (var (name, function) in scope.Functions)
        {
            _currentFunctionName = name;
            _currentFunction = function;
            foreach (var parameter in function.Parameters)
            {
                if (parameter.Type.GetType() != typeof(TypePlaceholder))
                {
                    continue;
                }

                var placeholder = (TypePlaceholder)parameter.Type;
                if (scope.Types.TryGetValue(placeholder.Name, out var found))
                {
                    parameter.Type = found;
                    scope.Bindings[parameter.Name] = found;
                    continue;
                }

                if (scope.Bindings.TryGetValue(placeholder.Name, out var binding))
                {
                    parameter.Type = (Type.Type)binding;
                    scope.Bindings[parameter.Name] = (Type.Type)binding;
                    continue;
                }

                throw new ParseException($"unknown predicate parameter type '{placeholder.Name}'");
            }

            LinkExpression(scope, function.Expression);
            _currentFunctionName = "";
            _currentFunction = null;
        }
    }

    public static void ShortCircuitRecursion(Scope scope, Function.Function function)
    {
        var series = function.Expression as Series;
        var last = series!.Expressions.Last();
        LinkExpression(scope, last);
        series.Type = last.Type;
    }

    public static void LinkExpression(Scope scope, Expression.Expression expression)
    {
        switch (expression)
        {
            case Literal _:
            case Wildcard _:
                // nop
                break;
            case Identifier identifier:
                if (scope.Bindings.TryGetValue(identifier.Name, out var variable))
                {
                    if (variable is Type.Type type_)
                    {
                        identifier.Type = type_;
                    }

                    break;
                }

                if (scope.Instances.TryGetValue(identifier.Name, out var instance))
                {
                    identifier.Type = instance.Type;
                    break;
                }

                if (scope.Types.TryGetValue(identifier.Name, out var type))
                {
                    identifier.Type = type;
                    break;
                }

                if (scope.Functions.TryGetValue(identifier.Name, out var func))
                {
                    identifier.Type = func.Expression.Type;
                    break;
                }

                throw new ResolveException($"unknown symbol '{identifier.Name}'");
            case Binding binding:
                if (scope.Bindings.TryGetValue("$", out var found))
                {
                    var _type = _callMany ? Type.Type.Tuple : (Type.Type)found;
                    binding.Type = _type;
                    scope.Bindings[binding.Name] = _type;
                    break;
                }

                throw new ResolveException($"search binding '{binding}' is not valid here");
            case Parenthetical parenthetical:
                LinkExpression(scope, parenthetical.Expression);
                parenthetical.Type = parenthetical.Expression.Type;
                if (parenthetical.Expression.ContainsBinding)
                {
                    parenthetical.ContainsBinding = true;
                    parenthetical.BindingName = parenthetical.Expression.BindingName;
                }

                break;
            case Tuple tuple:
                foreach (var e in tuple.Expressions)
                {
                    LinkExpression(scope, e);
                    if (e.ContainsBinding)
                    {
                        tuple.ContainsBinding = true;
                        tuple.BindingName = e.BindingName;
                    }
                }

                tuple.Type = Type.Type.Tuple;
                break;
            case Assign assign:
                if (!scope.Bindings.ContainsKey(assign.Variable))
                {
                    scope.Bindings[assign.Variable] = Type.Type.Variable;
                }

                LinkExpression(scope, assign.Value);
                assign.Type = assign.Value.Type;
                scope.Bindings[assign.Variable] = assign.Value.Type;
                break;
            case Call call:
                if (call.Functor == _currentFunctionName)
                {
                    ShortCircuitRecursion(scope, _currentFunction!);
                }

                if (call.Functor == "instance")
                {
                    call.Type = Type.Type.Boolean;
                    break;
                }

                if (scope.Schemata.TryGetValue(call.Functor, out var schema))
                {
                    if (call.Many)
                    {
                        _callMany = true;
                    }

                    expression.Type = Type.Type.Boolean;
                    var arguments = call.Arguments.ToList();
                    if (arguments.Count != schema.Parameters.Count)
                    {
                        throw new LinkException(
                            $"schema '{schema.Name}' takes {schema.Parameters.Count} parameters but {arguments.Count} arguments were provided");
                    }

                    for (var i = 0; i < arguments.Count; i++)
                    {
                        // TODO this loop is inelegant
                        var e = arguments[i];
                        if (e is Binding b)
                        {
                            var parameterType = schema.Parameters[i].Type;
                            scope.Bindings[b.Name] = parameterType;
                            b.Type = parameterType;
                        }

                        scope.Bindings["$"] = schema.Parameters[i].Type;
                        LinkExpression(scope, e);
                        scope.Bindings.Remove("$");
                    }

                    _callMany = false;
                    break;
                }

                if (scope.Functions.TryGetValue(call.Functor, out var function))
                {
                    foreach (var e in call.Arguments)
                    {
                        LinkExpression(scope, e);
                    }

                    expression.Type = function.Expression.Type;
                    break;
                }

                if (AllBuiltIns.ByName.TryGetValue(call.Functor, out var builtin))
                {
                    foreach (var e in call.Arguments)
                    {
                        LinkExpression(scope, e);
                    }

                    call.Type = builtin.Type;
                    break;
                }

                throw new ResolveException($"unknown functor '{call.Functor}'");
            case Unary unary:
                LinkExpression(scope, unary.Expression);
                if (unary.Expression.ContainsBinding)
                {
                    unary.ContainsBinding = true;
                    unary.BindingName = unary.Expression.BindingName;
                }

                switch (unary.Operator)
                {
                    case "-":
                        if (unary.Expression.Type.Equals(Type.Type.Int) ||
                            unary.Expression.Type.Equals(Type.Type.Float))
                        {
                            expression.Type = unary.Expression.Type;
                            break;
                        }

                        throw new ParseException(
                            $"invalid type '{unary.Expression.Type}' for unary operator '{unary.Operator}'");
                    case "!":
                        if (unary.Expression.Type.Equals(Type.Type.Boolean))
                        {
                            expression.Type = Type.Type.Boolean;
                            break;
                        }

                        throw new ParseException(
                            $"invalid type '{unary.Expression.Type}' for unary operator '{unary.Operator}'");
                    default:
                        throw new ParseException($"unknown unary operator '{unary.Operator}'");
                }

                break;
            case Infix infix:
                LinkExpression(scope, infix.Left);
                LinkExpression(scope, infix.Right);
                // TODO this could be wonky
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

                if (!infix.Left.Type.Equals(infix.Right.Type) && infix.Operator != "::" && infix.Operator != "^")
                {
                    throw new ParseException(
                        $"mismatched types '{infix.Left.Type}' and '{infix.Right.Type}' for '{infix.Operator}' infix expression");
                }

                switch (infix.Operator)
                {
                    case "::":
                        if (infix.Left is Binding)
                        {
                            infix.Type = Type.Type.Boolean;
                            break;
                        }

                        throw new ParseException(
                            $"invalid types '{infix.Left.GetType()}' and '{infix.Right.GetType()}' for '{infix.Operator}' infix expression");
                    case "+":
                    case "*":
                    case "~":
                    case "-":
                    case "/": // hmm
                    case "^":
                        if (infix.Left.Type.Equals(Type.Type.Int) ||
                            infix.Left.Type.Equals(Type.Type.Float) ||
                            infix.Left.Type.Equals(Type.Type.String) ||
                            infix.Left.Type.Equals(Type.Type.Tuple))
                        {
                            // TODO ensure left and right types are equal
                            infix.Type = infix.Left.Type;
                            break;
                        }

                        throw new ParseException(
                            $"invalid types '{infix.Left}' and '{infix.Right}' for '{infix.Operator}' infix expression");
                    case ">":
                    case "<":
                    case ">=":
                    case "<=":
                    case "==":
                    case "!=":
                        // TODO isA instead of equals?
                        if (infix.Left.Type.Equals(Type.Type.Int) ||
                            infix.Left.Type.Equals(Type.Type.Float))
                        {
                            expression.Type = Type.Type.Boolean;
                            break;
                        }

                        throw new ParseException(
                            $"invalid types '{infix.Left}' and '{infix.Right}' for '{infix.Operator}' infix expression");
                    case "and":
                    case "or":
                    case "xor":
                        if (infix.Left.Type.Equals(Type.Type.Boolean))
                        {
                            expression.Type = Type.Type.Boolean;
                            break;
                        }

                        throw new ParseException(
                            $"invalid types '{infix.Left}' and '{infix.Right}' for '{infix.Operator}' infix expression");
                    default:
                        throw new ParseException($"unknown infix operator '{infix.Operator}'");
                }

                break;
            case Series series:
                foreach (var e in series.Expressions)
                {
                    LinkExpression(scope, e);
                }

                series.Type = series.Expressions.Last().Type;
                break;
            case If _if:
                LinkExpression(scope, _if.Condition);
                LinkExpression(scope, _if.Body);
                _if.ElseIfs.ToList().ForEach(e => LinkExpression(scope, e));
                if (_if.Else is not null)
                {
                    LinkExpression(scope, _if.Else);
                }

                _if.Type = Type.Type.Boolean;
                break;
            case Struct _struct:
                var fields = _struct.Value.Values.ToList();
                foreach (var field in fields)
                {
                    if (field.Expression != null)
                    {
                        LinkExpression(scope, field.Expression);
                    }
                }

                break;
            case Accessor accessor:
                LinkExpression(scope, accessor.Structure);
                LinkExpression(scope, accessor.Indexer);
                break;
            case Lambda lambda:
                foreach (var parameter in lambda.Parameters)
                {
                    if (parameter.Type.GetType() != typeof(TypePlaceholder))
                    {
                        continue;
                    }

                    var placeholder = (TypePlaceholder)parameter.Type;
                    if (scope.Types.TryGetValue(placeholder.Name, out var found1))
                    {
                        parameter.Type = found1;
                        scope.Bindings[parameter.Name] = found1;
                        continue;
                    }

                    if (scope.Bindings.TryGetValue(placeholder.Name, out var binding))
                    {
                        parameter.Type = (Type.Type)binding;
                        scope.Bindings[parameter.Name] = (Type.Type)binding;
                        continue;
                    }

                    throw new ParseException($"unknown lambda parameter type '{placeholder.Name}'");
                }
                LinkExpression(scope, lambda.Implementation);
                break;
            default:
                throw new ParseException($"unknown expression '{expression}'");
        }
    }
}