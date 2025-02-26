namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Double;

public class DoubleEqualityExpression(double ethalonValue) : DoubleExpressionBase, IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualValueAsDouble = double.Parse(actualValue);
        return Math.Abs(actualValueAsDouble - ethalonValue) <= Epsilon;
    }
}
