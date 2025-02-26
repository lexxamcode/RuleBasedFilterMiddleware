namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.String;

public class StringEqualityExpression(string ethalonValue) : IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        return actualValue.Equals(ethalonValue);
    }
}
