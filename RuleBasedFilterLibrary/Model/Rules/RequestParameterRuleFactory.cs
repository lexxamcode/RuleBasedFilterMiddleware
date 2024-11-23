using RuleBasedFilterLibrary.Model.Rules.Base;
using RuleBasedFilterLibrary.Model.Rules.Base.ParameterComparison;
using RuleBasedFilterLibrary.Model.Rules.Raw;

namespace RuleBasedFilterLibrary.Model.Rules;

public static class RequestParameterRuleFactory
{
    public static RequestParameterRule CreateFromRawRequestParameterRule(RawRequestParameterRule rawRequestParameterRule)
    {
        var comparisonExpressionMembers = rawRequestParameterRule.ShouldBe.Split(" ");
        var parameterType = TypeResolver.GetTypeFromString(rawRequestParameterRule.Type);

        var requestParameterRule = new RequestParameterRule
        {
            ParameterType = parameterType,
            ComparisonType = ComparisonTypeFactory.CreateComparisonTypeFromString(comparisonExpressionMembers[0]),
            EthalonValue = Convert.ChangeType(comparisonExpressionMembers[1], parameterType)
        };

        return requestParameterRule;
    }
}