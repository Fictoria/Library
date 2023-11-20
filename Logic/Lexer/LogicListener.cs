//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Logic/Grammar/Logic.g4 by ANTLR 4.13.0

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
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
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
	/// Enter a parse tree produced by <see cref="LogicParser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArgument([NotNull] LogicParser.ArgumentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArgument([NotNull] LogicParser.ArgumentContext context);
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
