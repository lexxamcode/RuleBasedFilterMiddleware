namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Float;

public class FloatEqualityExpression(float ethalonValue) : FloatExpressionBase, IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualValueAsFloat = float.Parse(actualValue);
        return Math.Abs(actualValueAsFloat - ethalonValue) <= Epsilon;
    }
}
