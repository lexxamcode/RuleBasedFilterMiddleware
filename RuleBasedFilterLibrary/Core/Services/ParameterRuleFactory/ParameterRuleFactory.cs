using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;
using RuleBasedFilterLibrary.Core.Model.ParameterRules.Implementations;
using RuleBasedFilterLibrary.Core.Utils;
using RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;

namespace RuleBasedFilterLibrary.Core.Services.ParameterRuleFactory;

public class ParameterRuleFactory : IParameterRuleFactory
{
    public ParameterRuleBase CreateFromRawRequestParameterRule(RawParameterRule rawRequestParameterRule)
    {
        var comparisonExpressionMembers = rawRequestParameterRule.ShouldBe.Split(" ");
        var comparisonTypeString = comparisonExpressionMembers[0];
        var ethalonValueString = comparisonExpressionMembers[1];

        if (string.IsNullOrEmpty(comparisonTypeString))
            throw new Exception($"Invalid comparisonType in {rawRequestParameterRule.ShouldBe}");

        var parameterType = rawRequestParameterRule.Type;
        var comparisonType = ComparisonTypeFactory.CreateComparisonTypeFromString(comparisonTypeString);

        return parameterType switch
        {
            "decimal" => new ParameterRuleGeneric<decimal> { Name = rawRequestParameterRule.Name, EthalonValue = decimal.Parse(ethalonValueString), ComparisonType = comparisonType },
            "long" => new ParameterRuleGeneric<long> { Name = rawRequestParameterRule.Name, EthalonValue = long.Parse(ethalonValueString), ComparisonType = comparisonType },
            "double" => new DoubleParameterRule { Name = rawRequestParameterRule.Name, EthalonValue = double.Parse(ethalonValueString), ComparisonType = comparisonType },
            "float" => new ParameterRuleGeneric<float> { Name = rawRequestParameterRule.Name, EthalonValue = float.Parse(ethalonValueString), ComparisonType = comparisonType },
            "int" => new ParameterRuleGeneric<int>() { Name = rawRequestParameterRule.Name, EthalonValue = int.Parse(ethalonValueString), ComparisonType = comparisonType },
            "integer" => new ParameterRuleGeneric<int>() { Name = rawRequestParameterRule.Name, EthalonValue = int.Parse(ethalonValueString), ComparisonType = comparisonType },
            "char" => new ParameterRuleGeneric<char> { Name = rawRequestParameterRule.Name, EthalonValue = char.Parse(ethalonValueString), ComparisonType = comparisonType },
            "byte" => new ParameterRuleGeneric<byte> { Name = rawRequestParameterRule.Name, EthalonValue = byte.Parse(ethalonValueString), ComparisonType = comparisonType },
            "short" => new ParameterRuleGeneric<short> { Name = rawRequestParameterRule.Name, EthalonValue = short.Parse(ethalonValueString), ComparisonType = comparisonType },
            "bool" => new ParameterRuleGeneric<bool> { Name = rawRequestParameterRule.Name, EthalonValue = bool.Parse(ethalonValueString), ComparisonType = comparisonType },
            "boolean" => new ParameterRuleGeneric<bool> { Name = rawRequestParameterRule.Name, EthalonValue = bool.Parse(ethalonValueString), ComparisonType = comparisonType },
            "string" => new ParameterRuleGeneric<string> { Name = rawRequestParameterRule.Name, EthalonValue = ethalonValueString, ComparisonType = comparisonType },

            _ => throw new Exception($"Parameter type {parameterType} unknown")
        };
    }
}