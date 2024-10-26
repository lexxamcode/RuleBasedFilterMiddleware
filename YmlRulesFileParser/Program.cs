using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YmlRulesFileParser.Model.Rules.Base;

var fileContentAsString = File.ReadAllText("rulesConf.Yml");

Console.WriteLine(fileContentAsString);

var deserializer = new DeserializerBuilder()
    .WithNamingConvention(HyphenatedNamingConvention.Instance)
    .Build();

var rulesContainer = deserializer.Deserialize<RulesContainer>(fileContentAsString);
Console.WriteLine();
Console.WriteLine(rulesContainer.Rules.First().Name);

foreach (var parameter in rulesContainer.Rules.First().Parameters)
{
    Console.WriteLine(parameter.Key);

    Console.WriteLine(parameter.Value.When);
}