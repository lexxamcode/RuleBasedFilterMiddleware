using RuleBasedFilterLibrary.Model.Rules.Base.ParameterComparison;

namespace RuleBasedFilterLibraryTests.Model;

public class TypeResolverTests
{
    [Theory]
    [InlineData("int", typeof(int))]
    [InlineData("integer", typeof(int))]
    [InlineData("string", typeof(string))]
    [InlineData("double", typeof(double))]
    [InlineData("float", typeof(float))]
    [InlineData("bool", typeof(bool))]
    [InlineData("boolean", typeof(bool))]
    [InlineData("char", typeof(char))]
    [InlineData("byte", typeof(byte))]
    [InlineData("short", typeof(short))]
    [InlineData("long", typeof(long))]
    [InlineData("decimal", typeof(decimal))]
    public static void GetTypeFromString_KnownType_Success(string typeAsString, Type expectedType)
    {
        var actualType = TypeResolver.GetTypeFromString(typeAsString);

        Assert.Equal(expectedType, actualType);
    }

    [Fact]
    public static void GetTypeFromString_UnknownType_Error()
    {
        var unknownTypeAsString = "unknownType";

        Assert.Throws<ArgumentException>(() => TypeResolver.GetTypeFromString(unknownTypeAsString));
    }
}
