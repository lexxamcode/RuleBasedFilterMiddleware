using RuleBasedFilterLibrary.Core.Model.Expressions;
using RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Double;
using RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.DoubleInterval;
using System.Globalization;

namespace RuleBasedFilterLibrary.Core.Services.Expression.DoubleExpressions;

public class DoubleExpressionFactory
{
    private static readonly Dictionary<string, Func<string[], IExpression>> _doubleExpressions = new()
    {
        {"==", args => new DoubleEqualityExpression(double.Parse(args[0], CultureInfo.InvariantCulture)) },
        {"!=", args => new DoubleNotEqualExpression(double.Parse(args[0], CultureInfo.InvariantCulture)) },
        {">", args => new DoubleGreaterThanExpression(double.Parse(args[0], CultureInfo.InvariantCulture)) },
        {">=", args => new DoubleGreaterOrEqualThanExpression(double.Parse(args[0], CultureInfo.InvariantCulture)) },
        {"<", args => new DoubleLessThanExpression(double.Parse(args[0], CultureInfo.InvariantCulture)) },
        {"<=", args => new DoubleLessOrEqualThanExpression(double.Parse(args[0], CultureInfo.InvariantCulture)) },
        {"between", args => new DoubleIntervalExpression(double.Parse(args[0], CultureInfo.InvariantCulture), double.Parse(args[1], CultureInfo.InvariantCulture)) },
    };

    public static IExpression Create(string comparisonExpression)
    {
        var comparisonExpressionMembers = comparisonExpression.Split(' ');
        var comparisonOperator = comparisonExpressionMembers[0];
        var ethalonValues = comparisonExpressionMembers.Skip(1).ToArray();

        if (_doubleExpressions.TryGetValue(comparisonOperator, out var creator))
        {
            return creator(ethalonValues);
        }

        throw new ArgumentException($"Type double does not support operation {comparisonOperator}");
    }
}
