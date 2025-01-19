namespace RuleBasedFilterLibrary.Model.Rules.Base.ParameterComparison;

public static class ComparisonTypeFactory
{
    public static readonly Dictionary<string, ComparisonType> ComparisonTypeMappings = new() {
        { "==", ComparisonType.Equal },
        { "!=", ComparisonType.NotEqual },
        { ">", ComparisonType.GreaterThan },
        { ">=", ComparisonType.GreaterOrEqualTo },
        { "<", ComparisonType.LessThan },
        { "<=", ComparisonType.LessOrEqualTo },
        { "monotone", ComparisonType.Monotone },
        { "non-monotone", ComparisonType.NonMonotone }
    };

    public static ComparisonType CreateComparisonTypeFromString(string comparisonExpression)
    {
        var doesMappingExist = ComparisonTypeMappings.TryGetValue(comparisonExpression, out var mapping);

        return doesMappingExist ?
            mapping :
            throw new ArgumentException($"Invalid comparison expression {comparisonExpression}");
    }
}