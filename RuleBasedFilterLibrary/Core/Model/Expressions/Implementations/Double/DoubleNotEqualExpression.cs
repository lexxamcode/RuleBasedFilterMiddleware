using System.Globalization;

namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Double;

public class DoubleNotEqualExpression(double ethalonValue): DoubleExpressionBase, IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualValueAsDouble = double.Parse(actualValue, CultureInfo.InvariantCulture);
        return Math.Abs(actualValueAsDouble - ethalonValue) > Epsilon;
    }
}

