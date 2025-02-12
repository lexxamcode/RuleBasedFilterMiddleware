using RuleBasedFilterLibrary.Core.Model.Rules;
using RuleBasedFilterLibrary.Infrastructure.Model.RawRules;

namespace RuleBasedFilterLibrary.Core.Services.RuleFactory;

public interface IRuleFactory
{
    public IRule CreateRuleFromItsRawRepresentation(RawRule rawRequestRule);
}
