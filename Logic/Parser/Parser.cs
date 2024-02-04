using Fictoria.Logic.Evaluation;
using Fictoria.Logic.Exceptions;
using Fictoria.Logic.Expression;
using Fictoria.Logic.Fact;
using Fictoria.Logic.Lexer;
using Fictoria.Logic.Type;
using Tuple = Fictoria.Logic.Expression.Tuple;

namespace Fictoria.Logic.Parser;

public class Parser : LogicBaseVisitor<object>
{
    public override object VisitLogic(LogicParser.LogicContext context)
    {
        var statements = context.statement().Select(Visit);
        var scope = Builder.FromStatements(statements);

        return new Program(scope);
    }

    public override object VisitQuery(LogicParser.QueryContext context)
    {
        var expression = (Expression.Expression)Visit(context.expression());
        return new Query(expression);
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

    public override object VisitInstance(LogicParser.InstanceContext context)
    {
        var name = (Expression.Expression)Visit(context.name);
        var type = (Expression.Expression)Visit(context.of);
        return new Instance(name, type);
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
        var arguments = context.expression().Select(a => (Expression.Expression)Visit(a)).ToList();

        return new Fact.Fact(new SchemaPlaceholder(identifier), arguments);
    }

    public override object VisitAntifact(LogicParser.AntifactContext context)
    {
        var call = (Call)Visit(context.call());
        return new Antifact(call);
    }

    public override object VisitFunction(LogicParser.FunctionContext context)
    {
        var identifier = context.identifier().IDENTIFIER().GetText();
        var parameters = context.parameter().Select(p => (Parameter)Visit(p)).ToList();
        var series = (Series)Visit(context.series());
        return new Function.Function(identifier, parameters, series);
    }

    public override object VisitAction(LogicParser.ActionContext context)
    {
        var name = context.identifier().IDENTIFIER().GetText();
        var parameters = context.parameter().Select(p => (Parameter)Visit(p)).ToList();
        var @struct = (Struct)Visit(context.@struct());
        var space = @struct.Value["space"].Expression!;
        var cost = @struct.Value["cost"].Expression!;
        var conditions = @struct.Value["conditions"].Expression!;
        var locals = @struct.Value["locals"].Expression!;
        var terms = @struct.Value["terms"].Expression!;
        var effects = @struct.Value["effects"].Statements!;

        return new Action.Action(
            name,
            parameters,
            space,
            cost,
            conditions,
            locals,
            terms,
            effects
        );
    }

    public override object VisitStruct(LogicParser.StructContext context)
    {
        var fields = context.field().Select(f => ((string, Field))Visit(f)).ToList();
        return new Struct(context.GetText(), fields);
    }

    public override object VisitField(LogicParser.FieldContext context)
    {
        var key = (Literal)Visit(context.literalString());
        var statements = context.statement()?.Select(Visit).ToList();
        if (statements?.Count > 0)
        {
            var scope = Builder.FromStatements(statements);
            return ((string)key.Value, new Field(scope));
        }

        var expression = (Expression.Expression)Visit(context.series());
        if (expression != null)
        {
            return ((string)key.Value, new Field(expression));
        }

        throw new ParseException($"invalid field entry '{context.GetText()}'");
    }

    public override object VisitSeries(LogicParser.SeriesContext context)
    {
        var expressions = context.expression().Select(e => (Expression.Expression)Visit(e)).ToList();
        return new Series(context.GetText(), expressions);
    }

    public override object VisitLiteralBool(LogicParser.LiteralBoolContext context)
    {
        var value = context.GetText();
        if (bool.TryParse(value, out var b))
        {
            return new Literal(context.GetText(), b, Type.Type.Boolean);
        }

        throw new ParseException($"invalid boolean literal '{value}'");
    }

    public override object VisitLiteralInt(LogicParser.LiteralIntContext context)
    {
        var value = context.GetText();
        if (long.TryParse(value, out var b))
        {
            return new Literal(context.GetText(), b, Type.Type.Int);
        }

        throw new ParseException($"invalid integer literal '{value}'");
    }

    public override object VisitLiteralFloat(LogicParser.LiteralFloatContext context)
    {
        var value = context.GetText();
        if (double.TryParse(value, out var b))
        {
            return new Literal(context.GetText(), b, Type.Type.Float);
        }

        throw new ParseException($"invalid float literal '{value}'");
    }

    public override object VisitLiteralString(LogicParser.LiteralStringContext context)
    {
        var value = context.GetText();
        var inner = value.Substring(1, value.Length - 2);
        return new Literal(context.GetText(), inner, Type.Type.String);
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
        return new Tuple(context.GetText(), expressions);
    }

    public override object VisitCall(LogicParser.CallContext context)
    {
        var many = context.many() is not null;
        var identifier = context.identifier().IDENTIFIER().GetText();
        var arguments = context.expression().Select(a => (Expression.Expression)Visit(a)).ToList();

        return new Call(context.GetText(), identifier, arguments, many);
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

    public override object VisitIndexExpression(LogicParser.IndexExpressionContext context)
    {
        var left = (Expression.Expression)Visit(context.left);
        var right = (Expression.Expression)Visit(context.right);

        return new Accessor(context.GetText(), left, right);
    }

    public override object VisitAssign(LogicParser.AssignContext context)
    {
        var variable = context.identifier().IDENTIFIER().GetText();
        var expression = (Expression.Expression)Visit(context.expression());

        return new Assign(context.GetText(), variable, expression);
    }

    public override object VisitIf(LogicParser.IfContext context)
    {
        var ifs = new List<If>();
        foreach (var c in context.condition())
        {
            var condition = c.expression().Select(e => (Expression.Expression)Visit(e)).ToList();
            var body = c.block().expression().Select(e => (Expression.Expression)Visit(e)).ToList();
            var cx = new Series("condition", condition);
            var bx = new Series("body", body);
            ifs.Add(new If(c.GetText(), cx, bx, new List<If>(), null));
        }

        var main = ifs.First();
        var rest = ifs.Skip(1).ToList();
        var _else = context.block().expression().Select(e => (Expression.Expression)Visit(e)).ToList();
        var ex = new Series("else", _else);

        return new If(context.GetText(), main.Condition, main.Body, rest, ex);
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
                case "+":
                    variance = Variance.Covariant;
                    break;
                case "-":
                    variance = Variance.Contravariant;
                    break;
                default:
                    variance = Variance.Invariant;
                    break;
            }
        }

        return new Parameter(name, new TypePlaceholder(type), variance);
    }
}