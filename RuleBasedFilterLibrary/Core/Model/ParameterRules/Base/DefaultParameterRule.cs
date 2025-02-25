using RuleBasedFilterLibrary.Core.Model.Expressions;

namespace RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;

public class DefaultParameterRule(IExpression expression) : ParameterRuleBase
{
    public override bool CompareTo(string actualParameterValue)
    {
        return expression.MatchesExpression(actualParameterValue);
    }
}
