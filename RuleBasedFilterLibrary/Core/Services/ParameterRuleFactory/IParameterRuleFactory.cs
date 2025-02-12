using RuleBasedFilterLibrary.Core.Model.ParameterRules;
using RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;

namespace RuleBasedFilterLibrary.Core.Services.ParameterRuleFactory;

public interface IParameterRuleFactory
{
    public ParameterRule CreateFromRawRequestParameterRule(RawParameterRule rawRequestParameterRule);
}
