namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Float;

public class FloatLessOrEqualThanExpression(float ethalonValue) : FloatExpressionBase, IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualValueAsFloat = float.Parse(actualValue);
        return actualValueAsFloat <= ethalonValue;
    }
}
