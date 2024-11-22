namespace YmlRulesFileParser.Model.Rules.Base.ParameterComparison;

public static class ComparisonTypeFactory
{
    public static ComparisonType CreateComparisonTypeFromString(string comparisonExpression)
    {
        return comparisonExpression switch
        {
            "==" => ComparisonType.Equal,
            "!=" => ComparisonType.NotEqual,
            "<" => ComparisonType.LessThan,
            "<=" => ComparisonType.LessOrEqualTo,
            ">" => ComparisonType.GreaterThan,
            ">=" => ComparisonType.GreaterOrEqualTo,
            _ => throw new ArgumentException($"Invalid expression type {comparisonExpression}")
        };
    }
}
