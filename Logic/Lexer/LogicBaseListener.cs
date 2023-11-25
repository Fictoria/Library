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
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="ILogicListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
[System.Diagnostics.DebuggerNonUserCode]
[System.CLSCompliant(false)]
public partial class LogicBaseListener : ILogicListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.logic"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLogic([NotNull] LogicParser.LogicContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.logic"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLogic([NotNull] LogicParser.LogicContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement([NotNull] LogicParser.StatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement([NotNull] LogicParser.StatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.type"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterType([NotNull] LogicParser.TypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.type"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitType([NotNull] LogicParser.TypeContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.schema"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSchema([NotNull] LogicParser.SchemaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.schema"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSchema([NotNull] LogicParser.SchemaContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.fact"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFact([NotNull] LogicParser.FactContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.fact"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFact([NotNull] LogicParser.FactContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.argument"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArgument([NotNull] LogicParser.ArgumentContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.argument"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArgument([NotNull] LogicParser.ArgumentContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.function"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunction([NotNull] LogicParser.FunctionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.function"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunction([NotNull] LogicParser.FunctionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>callExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCallExpression([NotNull] LogicParser.CallExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>callExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCallExpression([NotNull] LogicParser.CallExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>bindingExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBindingExpression([NotNull] LogicParser.BindingExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>bindingExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBindingExpression([NotNull] LogicParser.BindingExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>tupleExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTupleExpression([NotNull] LogicParser.TupleExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>tupleExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTupleExpression([NotNull] LogicParser.TupleExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>infixExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterInfixExpression([NotNull] LogicParser.InfixExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>infixExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitInfixExpression([NotNull] LogicParser.InfixExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>wildcardExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterWildcardExpression([NotNull] LogicParser.WildcardExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>wildcardExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitWildcardExpression([NotNull] LogicParser.WildcardExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>assignExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssignExpression([NotNull] LogicParser.AssignExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>assignExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssignExpression([NotNull] LogicParser.AssignExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>identifierExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIdentifierExpression([NotNull] LogicParser.IdentifierExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>identifierExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIdentifierExpression([NotNull] LogicParser.IdentifierExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>parenExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParenExpression([NotNull] LogicParser.ParenExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>parenExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParenExpression([NotNull] LogicParser.ParenExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>literalExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLiteralExpression([NotNull] LogicParser.LiteralExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>literalExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLiteralExpression([NotNull] LogicParser.LiteralExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>unaryExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnaryExpression([NotNull] LogicParser.UnaryExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>unaryExpression</c>
	/// labeled alternative in <see cref="LogicParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnaryExpression([NotNull] LogicParser.UnaryExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.assign"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssign([NotNull] LogicParser.AssignContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.assign"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssign([NotNull] LogicParser.AssignContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.call"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCall([NotNull] LogicParser.CallContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.call"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCall([NotNull] LogicParser.CallContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.parenthetical"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParenthetical([NotNull] LogicParser.ParentheticalContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.parenthetical"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParenthetical([NotNull] LogicParser.ParentheticalContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.tuple"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTuple([NotNull] LogicParser.TupleContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.tuple"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTuple([NotNull] LogicParser.TupleContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.wildcard"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterWildcard([NotNull] LogicParser.WildcardContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.wildcard"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitWildcard([NotNull] LogicParser.WildcardContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.literal"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLiteral([NotNull] LogicParser.LiteralContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.literal"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLiteral([NotNull] LogicParser.LiteralContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.literalBool"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLiteralBool([NotNull] LogicParser.LiteralBoolContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.literalBool"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLiteralBool([NotNull] LogicParser.LiteralBoolContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.literalInt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLiteralInt([NotNull] LogicParser.LiteralIntContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.literalInt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLiteralInt([NotNull] LogicParser.LiteralIntContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.literalFloat"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLiteralFloat([NotNull] LogicParser.LiteralFloatContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.literalFloat"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLiteralFloat([NotNull] LogicParser.LiteralFloatContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.binding"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBinding([NotNull] LogicParser.BindingContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.binding"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBinding([NotNull] LogicParser.BindingContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.parameter"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParameter([NotNull] LogicParser.ParameterContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.parameter"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParameter([NotNull] LogicParser.ParameterContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.variance"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterVariance([NotNull] LogicParser.VarianceContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.variance"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitVariance([NotNull] LogicParser.VarianceContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="LogicParser.identifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIdentifier([NotNull] LogicParser.IdentifierContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="LogicParser.identifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIdentifier([NotNull] LogicParser.IdentifierContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace Fictoria.Logic.Lexer
