using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Fact;
using Fictoria.Logic.Lexer;
using Fictoria.Logic.Type;

namespace Fictoria.Logic.Parser;

public class Parser : LogicBaseVisitor<object>
{
    private Scope scope = new Scope();

    public override object VisitLogic(LogicParser.LogicContext context)
    {
        foreach (var type in Type.Type.BuiltIns)
        {
            scope.Types[type.Name] = type;
        }

        scope.Schemata["instance"] = new Schema("instance", new List<Parameter> { new("i", scope.Types["anything"]), new("t", scope.Types["anything"]) });
        
        var statements = context.statement().Select(s => Visit(s));
        foreach (var statement in statements)
        {
            switch (statement)
            {
                case Type.Type type:
                    scope.DefineType(type);
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
                case Function.Function function:
                    scope.DefineFunction(function);
                    break;
            }
        }

        return new Program(scope);
    }

    public override object VisitType(LogicParser.TypeContext context)
    {
        var identifiers = context.identifier().ToList();
        var type = identifiers[0];
        var parents = identifiers.Skip(1).ToList();

        return new Type.Type(
            type.IDENTIFIER().GetText(), 
            parents.Select(p => (Type.Type)new TypePlaceholder(p.IDENTIFIER().GetText())).ToList()
        );
    }

    public override object VisitSchema(LogicParser.SchemaContext context)
    {
        var identifier = context.identifier().IDENTIFIER().GetText();
        var parameters = context.parameter().Select(p => (Parameter)Visit(p)).ToList();

        return new Schema(identifier, parameters);
    }

    public override object VisitFact(LogicParser.FactContext context)
    {
        var identifier = context.identifier().IDENTIFIER().GetText();
        
        if (identifier == "instance")
        {
            if (context.argument().Length != 2)
            {
                throw new ParseException($"instance facts require exactly 2 arguments");
            }
            
            var instance = context.argument(0).identifier().IDENTIFIER().GetText();
            var type = context.argument(1).identifier().IDENTIFIER().GetText();
            scope.DefineInstance(new Instance(instance, new TypePlaceholder(type)));
            return new Fact.Fact(new SchemaPlaceholder("instance"), new List<Expression.Expression>());
        }
        
        var arguments = context.argument().Select(a => (Expression.Expression)Visit(a)).ToList();

        return new Fact.Fact(new SchemaPlaceholder(identifier), arguments);
    }

    public override object VisitFunction(LogicParser.FunctionContext context)
    {
        var identifier = context.identifier().IDENTIFIER().GetText();
        var parameters = context.parameter().Select(p => (Parameter)Visit(p)).ToList();
        var expressions = context.expression().Select(e => (Expression.Expression)Visit(e)).ToList();
        return new Function.Function(identifier, parameters, new Series(context.GetText(), expressions));
    }

    public override object VisitLiteralBool(LogicParser.LiteralBoolContext context)
    {
        var value = context.GetText();
        if (bool.TryParse(value, out var b))
        {
            return new Literal(context.GetText(), b, Fictoria.Logic.Type.Type.Boolean);
        }

        throw new ParseException($"invalid boolean literal '{value}'");
    }

    public override object VisitLiteralInt(LogicParser.LiteralIntContext context)
    {
        var value = context.GetText();
        if (long.TryParse(value, out var b))
        {
            return new Literal(context.GetText(), b, Fictoria.Logic.Type.Type.Int);
        }

        throw new ParseException($"invalid integer literal '{value}'");
    }
    
    public override object VisitLiteralFloat(LogicParser.LiteralFloatContext context)
    {
        var value = context.GetText();
        if (double.TryParse(value, out var b))
        {
            return new Literal(context.GetText(), b, Fictoria.Logic.Type.Type.Float);
        }

        throw new ParseException($"invalid float literal '{value}'");
    }

    public override object VisitIdentifier(LogicParser.IdentifierContext context)
    {
        var identifier = context.IDENTIFIER().GetText();

        return new Identifier(context.GetText(), identifier);
    }

    public override object VisitWildcard(LogicParser.WildcardContext context)
    {
        return new Wildcard();
    }

    public override object VisitBinding(LogicParser.BindingContext context)
    {
        var identifier = context.identifier().IDENTIFIER().GetText();
        return new Binding(context.GetText(), identifier);
    }

    public override object VisitParenthetical(LogicParser.ParentheticalContext context)
    {
        var expression = (Expression.Expression)Visit(context.expression());

        return new Parenthetical(context.GetText(), expression);
    }

    public override object VisitTuple(LogicParser.TupleContext context)
    {
        var expressions = context.expression().Select(e => (Expression.Expression)Visit(e)).ToList();
        return new Expression.Tuple(context.GetText(), expressions);
    }

    public override object VisitCall(LogicParser.CallContext context)
    {
        var identifier = context.identifier().IDENTIFIER().GetText();
        var arguments = context.expression().Select(a => (Expression.Expression)Visit(a)).ToList();

        return new Call(context.GetText(), identifier, arguments);
    }

    public override object VisitUnaryExpression(LogicParser.UnaryExpressionContext context)
    {
        var op = context.op.Text;
        var expression = (Expression.Expression)Visit(context.expression());

        return new Unary(context.GetText(), op, expression);
    }

    public override object VisitInfixExpression(LogicParser.InfixExpressionContext context)
    {
        var left = (Expression.Expression)Visit(context.left);
        var op = context.op.Text;
        var right = (Expression.Expression)Visit(context.right);

        return new Infix(context.GetText(), left, op, right);
    }

    public override object VisitAssign(LogicParser.AssignContext context)
    {
        var variable = context.identifier().IDENTIFIER().GetText();
        var expression = (Expression.Expression)Visit(context.expression());
        
        return new Assign(context.GetText(), variable, expression);
    }

    public override object VisitParameter(LogicParser.ParameterContext context)
    {
        var name = context.identifier()[0].IDENTIFIER().GetText();
        var type = context.identifier()[1].IDENTIFIER().GetText();
        var varianceContext = context.variance();
        Variance variance;
        if (varianceContext is null)
        {
            variance = Variance.Invariant;
        }
        else
        {
            var value = varianceContext.GetText();
            switch (value)
            {
                case "+": variance = Variance.Covariant; break;
                case "-": variance = Variance.Contravariant; break;
                default: variance = Variance.Invariant; break;
            }
        }

        return new Parameter(name, new TypePlaceholder(type), variance);
    }
}