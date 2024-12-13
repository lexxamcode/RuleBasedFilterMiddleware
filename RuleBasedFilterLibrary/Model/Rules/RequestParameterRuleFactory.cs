using RuleBasedFilterLibrary.Model.Rules.Base;
using RuleBasedFilterLibrary.Model.Rules.Base.ParameterComparison;
using RuleBasedFilterLibrary.Model.Rules.Raw;

namespace RuleBasedFilterLibrary.Model.Rules;

public static class RequestParameterRuleFactory
{
    public static RequestParameterRule CreateFromRawRequestParameterRule(RawRequestParameterRule rawRequestParameterRule)
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

        var requestParameterRule = new RequestParameterRule
        {
            Name = rawRequestParameterRule.Name,
            ParameterType = parameterType,
            ComparisonType = comparisonType,
            EthalonValue = ethalonValue
        };

        return requestParameterRule;
    }
}