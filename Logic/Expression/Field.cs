using System.Diagnostics.CodeAnalysis;
using Fictoria.Logic.Evaluation;

namespace Fictoria.Logic.Expression;

public class Field
{
    public Expression? Expression { get; }
    public Scope? Statements { get; }

    public Field(Expression expression)
    {
        Expression = expression;
    }

    public Field(Scope statements)
    {
        Statements = statements;
    }

    public bool TryGetExpression(out Expression expression)
    {
        if (Expression != null)
        {
            expression = Expression;
            return true;
        }

        expression = null!;
        return false;
    }

    public bool TryGetStatements(out Scope statements)
    {
        if (Statements != null)
        {
            statements = Statements;
            return true;
        }

        statements = null!;
        return false;
    }

    [ExcludeFromCodeCoverage]
    protected bool Equals(Field other)
    {
        return Equals(Expression, other.Expression) && Equals(Statements, other.Statements);
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Field)obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        return HashCode.Combine(Expression, Statements);
    }
}