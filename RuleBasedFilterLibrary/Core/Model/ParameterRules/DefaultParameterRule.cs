using RuleBasedFilterLibrary.Core.Model.Expressions;
using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;

namespace RuleBasedFilterLibrary.Core.Model.ParameterRules;

public class DefaultParameterRule(IExpression expression) : ParameterRuleBase
{
    public override bool Validate(Dictionary<string, string> arguments)
    {
        return expression.MatchesExpression(arguments[ArgumentName]);
    }
}
