using RuleBasedFilterLibrary.Core.Model.Rules;
using RuleBasedFilterLibrary.Core.Utils;
using RuleBasedFilterLibrary.Infrastructure.Model.RawRules;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RuleBasedFilterLibrary.Infrastructure.Services.RulesFileParsing;

public class RulesLoader : IRulesLoader
{
    public List<Rule> LoadRulesFromConfigurationFile(string configurationFilename)
    {
        var fileContentAsString = File.ReadAllText(configurationFilename);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(HyphenatedNamingConvention.Instance)
            .Build();

        var rawRulesContainer = deserializer.Deserialize<RawRulesContainer>(fileContentAsString);
        var rules = rawRulesContainer
            .RequestRules
            .Select(RuleFactory.CreateRequestRuleFromItsRawRepresentation)
            .ToList();

        return rules;
    }
}
