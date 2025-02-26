using RuleBasedFilterLibrary.Core.Model.ParameterRules;
using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;
using RuleBasedFilterLibrary.Core.Services.Expression;
using RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;

namespace RuleBasedFilterLibrary.Core.Services.ParameterRuleFactory;

public class ParameterRuleFactory : IParameterRuleFactory
{
    private static readonly Dictionary<string, Func<RawParameterRule, IParameterRule>> _parameterRules = new()
    {
        { "latitude", (rawParameterRule) => LatitudeParameterRuleFactory.Create(rawParameterRule) },
        { "longitude", (rawParameterRule) => LongitudeParameterRuleFactory.Create(rawParameterRule) }
    };

    public IParameterRule CreateFromRawRequestParameterRule(RawParameterRule rawRequestParameterRule)
    {
        if (_parameterRules.TryGetValue(rawRequestParameterRule.Type, out var creator))
            return creator(rawRequestParameterRule);

        return DefaultParameterRuleFactory.Create(rawRequestParameterRule);
    }
}