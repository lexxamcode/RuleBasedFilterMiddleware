using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YmlRulesFileParser.Model.Rules;
using YmlRulesFileParser.Model.Rules.Raw;

var fileContentAsString = File.ReadAllText("rulesConf.Yml");

var deserializer = new DeserializerBuilder()
    .WithNamingConvention(HyphenatedNamingConvention.Instance)
    .Build();

var rawRulesContainer = deserializer.Deserialize<RawRulesContainer>(fileContentAsString);

var rules = rawRulesContainer.RequestRules.Select(rule => RequestRuleFactory.CreateRequestRuleFromItsRawRepresentation(rule)).ToList();

Console.WriteLine(rules.Count);

foreach (var rule in rules)
{
    Console.WriteLine(rule.Name);
    Console.WriteLine(rule.HttpMethod);
    Console.WriteLine(rule.AccessPolicy);
    Console.WriteLine(rule.ParameterRules.Count);
}