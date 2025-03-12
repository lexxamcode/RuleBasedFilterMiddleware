using System.Globalization;

namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Float;

public class FloatEqualityExpression(float ethalonValue) : FloatExpressionBase, IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualValueAsFloat = float.Parse(actualValue, CultureInfo.InvariantCulture);
        return Math.Abs(actualValueAsFloat - ethalonValue) <= Epsilon;
    }
}
