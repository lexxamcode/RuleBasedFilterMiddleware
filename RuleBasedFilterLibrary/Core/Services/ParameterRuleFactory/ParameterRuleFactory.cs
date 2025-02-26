using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;
using RuleBasedFilterLibrary.Core.Services.Expression;
using RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;

namespace RuleBasedFilterLibrary.Core.Services.ParameterRuleFactory;

public class ParameterRuleFactory : IParameterRuleFactory
{
    public IParameterRule CreateFromRawRequestParameterRule(RawParameterRule rawRequestParameterRule)
    {
        var parameterType = rawRequestParameterRule.Type;

        var expressionFactory = new ExpressionFactory();
        var expression = expressionFactory.Create(parameterType, rawRequestParameterRule.ShouldBe);

        return new DefaultParameterRule(expression)
        {
            Name = rawRequestParameterRule.Name,
        };
    }
}