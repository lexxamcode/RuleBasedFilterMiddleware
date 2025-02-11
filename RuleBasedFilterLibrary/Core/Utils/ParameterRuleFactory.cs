using RuleBasedFilterLibrary.Core.Model.ParameterRules;
using RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;

namespace RuleBasedFilterLibrary.Core.Utils;

public static class ParameterRuleFactory
{
    public static ParameterRule CreateFromRawRequestParameterRule(RawParameterRule rawRequestParameterRule)
    {
        var comparisonExpressionMembers = rawRequestParameterRule.ShouldBe.Split(" ");
        var comparisonTypeString = comparisonExpressionMembers[0];
        string? ethalonValueString = null;

        try
        {
            ethalonValueString = comparisonExpressionMembers[1];
        }
        catch { }

        if (string.IsNullOrEmpty(comparisonTypeString))
            throw new Exception($"Invalid comparisonType in {rawRequestParameterRule.ShouldBe}");

        var parameterType = TypeResolver.GetTypeFromString(rawRequestParameterRule.Type);

        var comparisonType = ComparisonTypeFactory.CreateComparisonTypeFromString(comparisonTypeString);
        var ethalonValue = string.IsNullOrEmpty(ethalonValueString) ?
            null :
            Convert.ChangeType(ethalonValueString, parameterType);

        var requestParameterRule = new ParameterRule
        {
            Name = rawRequestParameterRule.Name,
            ParameterType = parameterType,
            ComparisonType = comparisonType,
            EthalonValue = ethalonValue
        };

        return requestParameterRule;
    }
}