﻿using YmlRulesFileParser.Model.Rules.Base;
using YmlRulesFileParser.Model.Rules.Base.ParameterComparison;
using YmlRulesFileParser.Model.Rules.Raw;

namespace YmlRulesFileParser.Model.Rules;

public static class RequestParameterRuleFactory
{
    public static RequestParameterRule CreateFromRawRequestParameterRule(RawRuleForRequestParameter rawRequestParameterRule)
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
