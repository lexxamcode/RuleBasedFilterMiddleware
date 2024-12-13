using RuleBasedFilterLibrary.Model.Rules.Base;
using RuleBasedFilterLibrary.Model.Rules.Raw;

namespace RuleBasedFilterLibrary.Model.Rules;

public static class RequestRuleFactory
{
    public static RequestRule CreateRequestRuleFromItsRawRepresentation(RawRequestRule rawRequestRule)
    {
        var accessPolicy = AccessPolicyFactory.CreateAccessPolicyFromString(rawRequestRule.AccessPolicy);
        var requestParameterRules = rawRequestRule.ParameterRules
            .Select(parameterRule => RequestParameterRuleFactory.CreateFromRawRequestParameterRule(parameterRule))
            .ToList();

        var requestRule = new RequestRule
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