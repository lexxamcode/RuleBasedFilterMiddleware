using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YmlRulesFileParser.Model.Rules;
using YmlRulesFileParser.Model.Rules.Base;
using YmlRulesFileParser.Model.Rules.Raw;

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
        var rules = rawRulesContainer.RequestRules.Select(rule => RequestRuleFactory.CreateRequestRuleFromItsRawRepresentation(rule)).ToList();

        return rules;
    }
}
