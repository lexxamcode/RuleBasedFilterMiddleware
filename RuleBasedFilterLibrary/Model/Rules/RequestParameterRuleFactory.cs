using RuleBasedFilterLibrary.Model.Rules.Base;
using RuleBasedFilterLibrary.Model.Rules.Base.ParameterComparison;
using RuleBasedFilterLibrary.Model.Rules.Raw;

namespace RuleBasedFilterLibrary.Model.Rules;

public static class RequestParameterRuleFactory
{
    public static RequestParameterRule CreateFromRawRequestParameterRule(RawRequestParameterRule rawRequestParameterRule)
    {
        var parameterType = TypeResolver.GetTypeFromString(rawRequestParameterRule.Type);

        var comparisonExpressionMembers = rawRequestParameterRule.ShouldBe.Split(" ");
        var comparisonType = ComparisonTypeFactory.CreateComparisonTypeFromString(comparisonExpressionMembers[0]);
        object? ethalonValue;

        try
        {
            ethalonValue = Convert.ChangeType(comparisonExpressionMembers[1], parameterType);
        }
        catch(Exception)
        {
            ethalonValue = null;
        }

        var requestParameterRule = new RequestParameterRule
        {
            ParameterType = parameterType,
            ComparisonType = comparisonType,
            EthalonValue = ethalonValue
        };

        return requestParameterRule;
    }
}