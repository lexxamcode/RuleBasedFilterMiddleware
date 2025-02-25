namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.FloatInterval;

public class FloatIntervalExpression(float leftBorder, float rightBorder) : IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualFloatValue = float.Parse(actualValue);

        var greaterThanLeftBorder = actualFloatValue >= leftBorder;
        var lessThanRightBorder = actualFloatValue <= rightBorder;

        return greaterThanLeftBorder && lessThanRightBorder;
    }
}
