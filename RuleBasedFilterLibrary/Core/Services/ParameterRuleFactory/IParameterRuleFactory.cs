using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;
using RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;

namespace RuleBasedFilterLibrary.Core.Services.ParameterRuleFactory;

public interface IParameterRuleFactory
{
    public IParameterRule CreateFromRawRequestParameterRule(RawParameterRule rawRequestParameterRule);
}
