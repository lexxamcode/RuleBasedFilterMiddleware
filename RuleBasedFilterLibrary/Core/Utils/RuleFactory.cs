using RuleBasedFilterLibrary.Core.Model.Rules;
using RuleBasedFilterLibrary.Infrastructure.Model.RawRules;

namespace RuleBasedFilterLibrary.Core.Utils;

public static class RuleFactory
{
    public static Rule CreateRequestRuleFromItsRawRepresentation(RawRule rawRequestRule)
    {
        var accessPolicy = AccessPolicyFactory.CreateAccessPolicyFromString(rawRequestRule.AccessPolicy);
        var requestParameterRules = rawRequestRule.ParameterRules
            .Select(parameterRule => ParameterRuleFactory.CreateFromRawRequestParameterRule(parameterRule))
            .ToList();

        var requestRule = new Rule
        {
            Name = rawRequestRule.Name,
            SourceIp = rawRequestRule.SourceIp,
            AccessPolicy = accessPolicy,
            HttpMethod = rawRequestRule.RequestMethod,
            ParameterRules = requestParameterRules,
            Endpoint = rawRequestRule.ForEndpoint
        };

        return requestRule;
    }
}