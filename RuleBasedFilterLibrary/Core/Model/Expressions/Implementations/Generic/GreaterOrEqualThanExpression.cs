using RuleBasedFilterLibrary.Core.Model.Comparison;

namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Generic;

public class GreaterOrEqualThanExpression<Type>(IComparer comparer, Type ethalonValue) : IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualValueWithGivenType = Convert.ChangeType(actualValue, typeof(Type));
        return comparer.Compare(actualValueWithGivenType, ethalonValue) >= 0;
    }
}
