namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.String;

public class StringNotEqualExpression(string ethalonValue) : IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        return actualValue.Equals(ethalonValue);
    }
}