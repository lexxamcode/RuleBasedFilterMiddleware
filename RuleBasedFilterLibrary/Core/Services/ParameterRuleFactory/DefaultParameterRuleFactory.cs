using RuleBasedFilterLibrary.Core.Model.ParameterRules;
using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;
using RuleBasedFilterLibrary.Core.Services.Expression;
using RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;

namespace RuleBasedFilterLibrary.Core.Services.ParameterRuleFactory;

public class DefaultParameterRuleFactory
{
    public static IParameterRule Create(RawParameterRule rawParameterRule)
    {
        var parameterType = rawParameterRule.ArgumentType;

        var expressionFactory = new ExpressionFactory();
        var expression = expressionFactory.Create(parameterType, rawParameterRule.ArgumentShouldBe);

        return new DefaultParameterRule(expression)
        {
            Name = rawParameterRule.Name,
            ArgumentName = rawParameterRule.ArgumentName,
        };
    }
}
