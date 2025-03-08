namespace RuleBasedFilterLibrary.Core.Model.Expressions;

public interface IExpression
{
    public bool MatchesExpression(string actualValue);
}
