using RuleBasedFilterLibrary.Model.Rules;
using RuleBasedFilterLibrary.Model.Rules.Raw;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

var fileContentAsString = File.ReadAllText("rulesConf.Yml");

var deserializer = new DeserializerBuilder()
    .WithNamingConvention(HyphenatedNamingConvention.Instance)
    .Build();

// rulesConf.yml deserialization into Rules
var rawRulesContainer = deserializer.Deserialize<RawRequestRulesContainer>(fileContentAsString);
var rules = rawRulesContainer.RequestRules.Select(rule => RequestRuleFactory.CreateRequestRuleFromItsRawRepresentation(rule)).ToList();

Console.WriteLine(rules.Count);

foreach (var rule in rules)
{
    Console.WriteLine(rule.Name);
    Console.WriteLine(rule.HttpMethod);
    Console.WriteLine(rule.AccessPolicy);
    Console.WriteLine(rule.ParameterRules.Count);
}