using YmlRulesFileParser.Model.Rules.Base;

namespace YmlRulesFileParser.Model.Rules;

public static class AccessPolicyFactory
{
    public static AccessPolicy CreateAccessPolicyFromString(string accessPolicyStringRepresentation)
    {
        return accessPolicyStringRepresentation switch
        {
            "allow" => AccessPolicy.Allow,
            "deny" => AccessPolicy.Deny,
            _ => throw new ArgumentException($"Invalid access policy name: {accessPolicyStringRepresentation}")
        };
    }
}
