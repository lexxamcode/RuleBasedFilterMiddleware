using RuleBasedFilterLibrary.Model.Rules.Base.ParameterComparison;

namespace RuleBasedFilterLibraryTests.Model;

public class ComparisonTypeFactoryTests
{
    [Theory]
    [InlineData("==", ComparisonType.Equal)]
    [InlineData("!=", ComparisonType.NotEqual)]
    [InlineData("<", ComparisonType.LessThan)]
    [InlineData("<=", ComparisonType.LessOrEqualTo)]
    [InlineData(">", ComparisonType.GreaterThan)]
    [InlineData(">=", ComparisonType.GreaterOrEqualTo)]
    [InlineData("monotone", ComparisonType.Monotone)]
    [InlineData("non-monotone", ComparisonType.NonMonotone)]
    public static void CreateComparisonTypeFromString_KnownType_Success(string comparisonTypeExpression, ComparisonType expectedComparisonType)
    {
        var actualComparisonType = ComparisonTypeFactory
            .CreateComparisonTypeFromString(comparisonTypeExpression);

        Assert.Equal(expectedComparisonType, actualComparisonType);
    }

    [Fact]
    public static void CreateComparisonTypeFromString_UnknownType_Error()
    {
        var unknownComparisonTypeString = ">>=";

        Assert.Throws<ArgumentException>(() =>
            ComparisonTypeFactory.CreateComparisonTypeFromString(unknownComparisonTypeString));
    }
}
