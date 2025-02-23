using RuleBasedFilterLibrary.Core.Model.Comparison;
using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;
using System.Globalization;

namespace RuleBasedFilterLibrary.Core.Model.ParameterRules.Implementations;

public class DoubleParameterRule : ParameterRuleGeneric<double>
{
    private const double Epsilon = 0.00001;
    public override bool CompareTo(string actualParameterValue)
    {
        var actualValueWithGivenType = double.Parse(actualParameterValue, CultureInfo.InvariantCulture);

        return ComparisonType switch
        {
            ComparisonType.Equal => Math.Abs(actualValueWithGivenType - EthalonValue) < Epsilon,
            ComparisonType.NotEqual => Math.Abs(actualValueWithGivenType - EthalonValue) > Epsilon,
            ComparisonType.LessThan => actualValueWithGivenType < EthalonValue,
            ComparisonType.LessOrEqualTo => actualValueWithGivenType <= EthalonValue,
            ComparisonType.GreaterThan => actualValueWithGivenType > EthalonValue,
            ComparisonType.GreaterOrEqualTo => actualValueWithGivenType >= EthalonValue,
            _ => false
        };
    }
}
