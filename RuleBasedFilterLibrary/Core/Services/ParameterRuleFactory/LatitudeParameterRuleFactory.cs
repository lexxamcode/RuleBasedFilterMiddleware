﻿using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;
using RuleBasedFilterLibrary.Core.Model.ParameterRules;
using RuleBasedFilterLibrary.Core.Services.Expression;
using RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;

namespace RuleBasedFilterLibrary.Core.Services.ParameterRuleFactory;

public class LatitudeParameterRuleFactory
{
    public static IParameterRule Create(RawParameterRule rawParameterRule)
    {
        var parameterType = "double";

        var expressionFactory = new ExpressionFactory();
        var expression = expressionFactory.Create(parameterType, rawParameterRule.ArgumentShouldBe);

        return new LatitudeParameterRule(expression)
        {
            Name = rawParameterRule.Name,
            ArgumentName = rawParameterRule.ArgumentName,
        };
    }
}
