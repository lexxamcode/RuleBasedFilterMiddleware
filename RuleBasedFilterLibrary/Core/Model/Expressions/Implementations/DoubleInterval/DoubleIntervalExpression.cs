using RuleBasedFilterLibrary.Core.Model.Comparison;

namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.DoubleInterval;

class DoubleIntervalExpression(double leftBorder, double rightBorder) : IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualDoubleValue = double.Parse(actualValue);

        var greaterThanLeftBorder = actualDoubleValue >= leftBorder;
        var lessThanRightBorder = actualDoubleValue <= rightBorder;

        return greaterThanLeftBorder && lessThanRightBorder;
    }
}