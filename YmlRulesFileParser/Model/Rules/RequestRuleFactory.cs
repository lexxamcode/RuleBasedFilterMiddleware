using YmlRulesFileParser.Model.Rules.Base;
using YmlRulesFileParser.Model.Rules.Raw;

namespace YmlRulesFileParser.Model.Rules;

public static class RequestRuleFactory
{
    public static RequestRule CreateRequestRuleFromItsRawRepresentation(RawRuleForRequest rawRequestRule)
    {
        var accessPolicy = AccessPolicyFactory.CreateAccessPolicyFromString(rawRequestRule.AccessPolicy);
        var requestParameterRules = rawRequestRule.ParameterRules.ToDictionary(w => w.Key, w => RequestParameterRuleFactory.CreateFromRawRequestParameterRule(w.Value));

        var requestRule = new RequestRule
        {
            Name = rawRequestRule.Name,
            SourceIp = rawRequestRule.SourceIp,
            AccessPolicy = accessPolicy,
            HttpMethod = rawRequestRule.Method,
            ParameterRules = requestParameterRules
        };

        return requestRule;
    }
}
