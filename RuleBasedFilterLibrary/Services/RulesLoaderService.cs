using RuleBasedFilterLibrary.Model.Rules;
using RuleBasedFilterLibrary.Model.Rules.Base;
using RuleBasedFilterLibrary.Model.Rules.Raw;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RuleBasedFilterLibrary.Services;

public class RulesLoaderService : IRulesLoaderService
{
    public List<RequestRule> LoadRulesFromConfigurationFile(string configurationFilename)
    {
        var fileContentAsString = File.ReadAllText(configurationFilename);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(HyphenatedNamingConvention.Instance)
            .Build();

        var rawRulesContainer = deserializer.Deserialize<RawRequestRulesContainer>(fileContentAsString);
        var rules = rawRulesContainer
            .RequestRules
            .Select(RequestRuleFactory.CreateRequestRuleFromItsRawRepresentation)
            .ToList();

        return rules;
    }
}
