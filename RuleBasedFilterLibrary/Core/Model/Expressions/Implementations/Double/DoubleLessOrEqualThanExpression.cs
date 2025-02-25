namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Double;

public class DoubleLessOrEqualThanExpression(double ethalonValue) : IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualValueAsDouble = double.Parse(actualValue);
        return actualValueAsDouble <= ethalonValue;
    }
}

