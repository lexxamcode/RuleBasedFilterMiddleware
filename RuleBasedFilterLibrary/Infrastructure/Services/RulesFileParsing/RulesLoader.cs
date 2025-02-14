using RuleBasedFilterLibrary.Core.Model.Rules;
using RuleBasedFilterLibrary.Core.Services.RuleFactory;
using RuleBasedFilterLibrary.Infrastructure.Model.RawRules;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RuleBasedFilterLibrary.Infrastructure.Services.RulesFileParsing;

public class RulesLoader(IRuleFactory ruleFactory) : IRulesLoader
{
    public List<IRule> LoadRulesFromConfigurationFile(string configurationFilename)
    {
        var fileContentAsString = File.ReadAllText(configurationFilename);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(HyphenatedNamingConvention.Instance)
            .Build();

        var rawRulesContainer = deserializer.Deserialize<RawRulesContainer>(fileContentAsString);
        var rules = rawRulesContainer
            .Rules
            .Select(ruleFactory.CreateRuleFromItsRawRepresentation)
            .ToList();

        return rules;
    }
}
