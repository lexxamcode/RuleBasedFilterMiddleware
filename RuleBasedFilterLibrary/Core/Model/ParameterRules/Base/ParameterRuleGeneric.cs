using RuleBasedFilterLibrary.Core.Model.Comparison;

namespace RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;

public class ParameterRuleGeneric<Type> : ParameterRuleBase where Type: IConvertible
{
    public Type? EthalonValue { get; set; }
    public ComparisonType ComparisonType { get; set; }

    public override bool CompareTo(string actualParameterValue)
    {
        if (EthalonValue is null)
            return false;

        var actualValueWithGivenType = Convert.ChangeType(actualParameterValue, typeof(Type));
        var ethalonValueWithGivenType = Convert.ChangeType(EthalonValue, typeof(Type));

        if (actualValueWithGivenType is null || ethalonValueWithGivenType is null)
            throw new ArgumentException("actualValue or EthalonValue have invalid type");

        return ComparisonType switch
        {
            ComparisonType.Equal => CastToIComparableAndCompare(actualValueWithGivenType, ethalonValueWithGivenType) == 0,
            ComparisonType.NotEqual => CastToIComparableAndCompare(actualValueWithGivenType, ethalonValueWithGivenType) != 0,
            ComparisonType.LessThan => CastToIComparableAndCompare(actualValueWithGivenType, ethalonValueWithGivenType) < 0,
            ComparisonType.LessOrEqualTo => CastToIComparableAndCompare(actualValueWithGivenType, ethalonValueWithGivenType) <= 0,
            ComparisonType.GreaterThan => CastToIComparableAndCompare(actualValueWithGivenType, ethalonValueWithGivenType) > 0,
            ComparisonType.GreaterOrEqualTo => CastToIComparableAndCompare(actualValueWithGivenType, ethalonValueWithGivenType) >= 0,
            _ => false
        };
    }

    protected virtual int CastToIComparableAndCompare(object actualValue, object ethalonValue)
    {
        if (actualValue is not IComparable actualValueAsComparable ||
            ethalonValue is not IComparable ethalonValueAsComparable)
            throw new ArgumentException("actualValue or ethalonValue are not IComparable");

        return actualValueAsComparable.CompareTo(ethalonValueAsComparable);
    }
}
