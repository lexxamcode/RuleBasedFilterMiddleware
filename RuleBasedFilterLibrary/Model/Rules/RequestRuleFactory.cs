using RuleBasedFilterLibrary.Model.Rules.Base;
using RuleBasedFilterLibrary.Model.Rules.Raw;

namespace RuleBasedFilterLibrary.Model.Rules;

public static class RequestRuleFactory
{
    public static RequestRule CreateRequestRuleFromItsRawRepresentation(RawRequestRule rawRequestRule)
    {
        var accessPolicy = AccessPolicyFactory.CreateAccessPolicyFromString(rawRequestRule.AccessPolicy);
        var requestParameterRules = rawRequestRule.ParameterRules.ToDictionary(w => w.Key, w => RequestParameterRuleFactory.CreateFromRawRequestParameterRule(w.Value));

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