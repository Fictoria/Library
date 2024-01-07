//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Logic/Grammar/Logic.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Fictoria.Logic.Lexer {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="LogicParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface ILogicVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.logic"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogic([NotNull] LogicParser.LogicContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] LogicParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] LogicParser.TypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.instance"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInstance([NotNull] LogicParser.InstanceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.schema"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSchema([NotNull] LogicParser.SchemaContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.fact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFact([NotNull] LogicParser.FactContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.antifact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAntifact([NotNull] LogicParser.AntifactContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunction([NotNull] LogicParser.FunctionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.action"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAction([NotNull] LogicParser.ActionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.struct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStruct([NotNull] LogicParser.StructContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitField([NotNull] LogicParser.FieldContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.series"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSeries([NotNull] LogicParser.SeriesContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>assignExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignExpression([NotNull] LogicParser.AssignExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>identifierExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifierExpression([NotNull] LogicParser.IdentifierExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>parenExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenExpression([NotNull] LogicParser.ParenExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>callExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCallExpression([NotNull] LogicParser.CallExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>bindingExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBindingExpression([NotNull] LogicParser.BindingExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>structExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructExpression([NotNull] LogicParser.StructExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>tupleExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTupleExpression([NotNull] LogicParser.TupleExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>infixExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInfixExpression([NotNull] LogicParser.InfixExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>wildcardExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWildcardExpression([NotNull] LogicParser.WildcardExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>indexExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIndexExpression([NotNull] LogicParser.IndexExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>literalExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralExpression([NotNull] LogicParser.LiteralExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ifExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfExpression([NotNull] LogicParser.IfExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>unaryExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryExpression([NotNull] LogicParser.UnaryExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIf([NotNull] LogicParser.IfContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondition([NotNull] LogicParser.ConditionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] LogicParser.BlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.assign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssign([NotNull] LogicParser.AssignContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCall([NotNull] LogicParser.CallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.parenthetical"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthetical([NotNull] LogicParser.ParentheticalContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.tuple"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTuple([NotNull] LogicParser.TupleContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.wildcard"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWildcard([NotNull] LogicParser.WildcardContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral([NotNull] LogicParser.LiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.literalBool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralBool([NotNull] LogicParser.LiteralBoolContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.literalInt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralInt([NotNull] LogicParser.LiteralIntContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.literalFloat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralFloat([NotNull] LogicParser.LiteralFloatContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.literalString"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralString([NotNull] LogicParser.LiteralStringContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.binding"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinding([NotNull] LogicParser.BindingContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParameter([NotNull] LogicParser.ParameterContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.many"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMany([NotNull] LogicParser.ManyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.variance"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariance([NotNull] LogicParser.VarianceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="LogicParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifier([NotNull] LogicParser.IdentifierContext context);
}
} // namespace Fictoria.Logic.Lexer
