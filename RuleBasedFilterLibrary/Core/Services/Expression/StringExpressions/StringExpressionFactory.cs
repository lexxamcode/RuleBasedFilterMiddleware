using RuleBasedFilterLibrary.Core.Model.Expressions;
using RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.String;

namespace RuleBasedFilterLibrary.Core.Services.Expression.StringExpressions;

public class StringExpressionFactory
{
    private static readonly Dictionary<string, Func<string[], IExpression>> _stringExpressions = new()
    {
        {"==", args => new StringEqualityExpression(args[0]) },
        {"!=", args => new StringNotEqualExpression(args[0]) },
    };

    public static IExpression Create(string comparisonExpression)
    {
        var comparisonExpressionMembers = comparisonExpression.Split(' ');
        var comparisonOperator = comparisonExpressionMembers[0];
        var ethalonValues = comparisonExpressionMembers.Skip(1).ToArray();

        if (_stringExpressions.TryGetValue(comparisonOperator, out var creator))
        {
            return creator(ethalonValues);
        }

        throw new ArgumentException($"Type string does not support operation {comparisonOperator}");
    }
}
