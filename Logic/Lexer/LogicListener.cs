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
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="LogicParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface ILogicListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.logic"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLogic([NotNull] LogicParser.LogicContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.logic"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLogic([NotNull] LogicParser.LogicContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] LogicParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] LogicParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.query"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterQuery([NotNull] LogicParser.QueryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.query"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitQuery([NotNull] LogicParser.QueryContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] LogicParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] LogicParser.TypeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.instance"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInstance([NotNull] LogicParser.InstanceContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.instance"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInstance([NotNull] LogicParser.InstanceContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.schema"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSchema([NotNull] LogicParser.SchemaContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.schema"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSchema([NotNull] LogicParser.SchemaContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.index"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIndex([NotNull] LogicParser.IndexContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.index"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIndex([NotNull] LogicParser.IndexContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.fact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFact([NotNull] LogicParser.FactContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.fact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFact([NotNull] LogicParser.FactContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.antifact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAntifact([NotNull] LogicParser.AntifactContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.antifact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAntifact([NotNull] LogicParser.AntifactContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction([NotNull] LogicParser.FunctionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction([NotNull] LogicParser.FunctionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.action"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAction([NotNull] LogicParser.ActionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.action"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAction([NotNull] LogicParser.ActionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.struct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStruct([NotNull] LogicParser.StructContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.struct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStruct([NotNull] LogicParser.StructContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterField([NotNull] LogicParser.FieldContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitField([NotNull] LogicParser.FieldContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.series"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSeries([NotNull] LogicParser.SeriesContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.series"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSeries([NotNull] LogicParser.SeriesContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>assignExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignExpression([NotNull] LogicParser.AssignExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>assignExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignExpression([NotNull] LogicParser.AssignExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>identifierExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdentifierExpression([NotNull] LogicParser.IdentifierExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>identifierExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdentifierExpression([NotNull] LogicParser.IdentifierExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>parenExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenExpression([NotNull] LogicParser.ParenExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>parenExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenExpression([NotNull] LogicParser.ParenExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>callExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCallExpression([NotNull] LogicParser.CallExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>callExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCallExpression([NotNull] LogicParser.CallExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>bindingExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBindingExpression([NotNull] LogicParser.BindingExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>bindingExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBindingExpression([NotNull] LogicParser.BindingExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>structExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStructExpression([NotNull] LogicParser.StructExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>structExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStructExpression([NotNull] LogicParser.StructExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>tupleExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTupleExpression([NotNull] LogicParser.TupleExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>tupleExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTupleExpression([NotNull] LogicParser.TupleExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>infixExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInfixExpression([NotNull] LogicParser.InfixExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>infixExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInfixExpression([NotNull] LogicParser.InfixExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>wildcardExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWildcardExpression([NotNull] LogicParser.WildcardExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>wildcardExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWildcardExpression([NotNull] LogicParser.WildcardExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>indexExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIndexExpression([NotNull] LogicParser.IndexExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>indexExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIndexExpression([NotNull] LogicParser.IndexExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>literalExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteralExpression([NotNull] LogicParser.LiteralExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>literalExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteralExpression([NotNull] LogicParser.LiteralExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ifExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfExpression([NotNull] LogicParser.IfExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ifExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfExpression([NotNull] LogicParser.IfExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>unaryExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnaryExpression([NotNull] LogicParser.UnaryExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>unaryExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnaryExpression([NotNull] LogicParser.UnaryExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIf([NotNull] LogicParser.IfContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIf([NotNull] LogicParser.IfContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondition([NotNull] LogicParser.ConditionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondition([NotNull] LogicParser.ConditionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] LogicParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] LogicParser.BlockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.assign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssign([NotNull] LogicParser.AssignContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.assign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssign([NotNull] LogicParser.AssignContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCall([NotNull] LogicParser.CallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCall([NotNull] LogicParser.CallContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.using"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUsing([NotNull] LogicParser.UsingContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.using"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUsing([NotNull] LogicParser.UsingContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.parenthetical"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenthetical([NotNull] LogicParser.ParentheticalContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.parenthetical"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenthetical([NotNull] LogicParser.ParentheticalContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.tuple"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTuple([NotNull] LogicParser.TupleContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.tuple"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTuple([NotNull] LogicParser.TupleContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.wildcard"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWildcard([NotNull] LogicParser.WildcardContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.wildcard"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWildcard([NotNull] LogicParser.WildcardContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] LogicParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] LogicParser.LiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.literalBool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteralBool([NotNull] LogicParser.LiteralBoolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.literalBool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteralBool([NotNull] LogicParser.LiteralBoolContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.literalInt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteralInt([NotNull] LogicParser.LiteralIntContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.literalInt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteralInt([NotNull] LogicParser.LiteralIntContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.literalFloat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteralFloat([NotNull] LogicParser.LiteralFloatContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.literalFloat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteralFloat([NotNull] LogicParser.LiteralFloatContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.literalString"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteralString([NotNull] LogicParser.LiteralStringContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.literalString"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteralString([NotNull] LogicParser.LiteralStringContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.binding"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinding([NotNull] LogicParser.BindingContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.binding"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinding([NotNull] LogicParser.BindingContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParameter([NotNull] LogicParser.ParameterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParameter([NotNull] LogicParser.ParameterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.many"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMany([NotNull] LogicParser.ManyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.many"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMany([NotNull] LogicParser.ManyContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.variance"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariance([NotNull] LogicParser.VarianceContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.variance"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariance([NotNull] LogicParser.VarianceContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdentifier([NotNull] LogicParser.IdentifierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdentifier([NotNull] LogicParser.IdentifierContext context);
}
} // namespace Fictoria.Logic.Lexer
