using RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Float;
using RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.FloatInterval;
using RuleBasedFilterLibrary.Core.Model.Expressions;

namespace RuleBasedFilterLibrary.Core.Services.Expression.FloatExpressions;

public class FloatExpressionFactory
{
    private static readonly Dictionary<string, Func<string[], IExpression>> _floatExpressions = new()
    {
        {"==", args => new FloatEqualityExpression(float.Parse(args[0])) },
        {"!=", args => new FloatNotEqualExpression(float.Parse(args[0])) },
        {">", args => new FloatGreaterThanExpression(float.Parse(args[0])) },
        {">=", args => new FloatGreaterOrEqualThanExpression(float.Parse(args[0])) },
        {"<", args => new FloatLessThanExpression(float.Parse(args[0])) },
        {"<=", args => new FloatLessOrEqualThanExpression(float.Parse(args[0])) },
        {"between", args => new FloatIntervalExpression(float.Parse(args[0]), float.Parse(args[1])) },
    };

    public static IExpression Create(string comparisonExpression)
    {
        var comparisonExpressionMembers = comparisonExpression.Split(' ');
        var comparisonOperator = comparisonExpressionMembers[0];
        var ethalonValues = comparisonExpressionMembers.Skip(1).ToArray();

        if (_floatExpressions.TryGetValue(comparisonOperator, out var creator))
        {
            return creator(ethalonValues);
        }

        throw new ArgumentException($"Type float does not support operation {comparisonOperator}");
    }
}