using RuleBasedFilterLibrary.Core.Model.Comparison;

namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.GenericInterval;

public class IntervalExpression<Type>(IComparer comparer, Type leftBorder, Type rightBorder) : IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualValueWithGivenType = Convert.ChangeType(actualValue, typeof(Type));

        var greaterThanLeftBorder = comparer.Compare(actualValueWithGivenType, leftBorder) >= 0;
        var lessThanRightBorder = comparer.Compare(actualValueWithGivenType, rightBorder) <= 0;
        
        return greaterThanLeftBorder && lessThanRightBorder;
    }
}