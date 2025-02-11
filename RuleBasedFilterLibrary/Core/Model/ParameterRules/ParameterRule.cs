using RuleBasedFilterLibrary.Core.Model.ComparisonTypes;

namespace RuleBasedFilterLibrary.Core.Model.ParameterRules;

/// <summary>
/// Правило для отдельного параметра
/// </summary>
public class ParameterRule
{
    /// <summary>
    /// Название параметра
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Тип параметра
    /// </summary>
    public Type ParameterType { get; set; } = typeof(object);

    /// <summary>
    /// Эталонное значение параметра
    /// </summary>
    public object? EthalonValue { get; set; }

    /// <summary>
    /// Логическое условие для данного параметра
    /// </summary>
    public ComparisonType ComparisonType { get; set; }

    public bool CompareTo(object actualValue)
    {
        if (EthalonValue is null)
            return false;

        var actualValueWithGivenType = Convert.ChangeType(actualValue, ParameterType);
        var ethalonValueWithGivenType = Convert.ChangeType(EthalonValue, ParameterType);

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

    private static int CastToIComparableAndCompare(object actualValue, object ethalonValue)
    {
        if (actualValue is not IComparable actualValueAsComparable ||
            ethalonValue is not IComparable ethalonValueAsComparable)
            throw new ArgumentException("actualValue or ethalonValue are not IComparable");

        return actualValueAsComparable.CompareTo(ethalonValueAsComparable);
    }
}