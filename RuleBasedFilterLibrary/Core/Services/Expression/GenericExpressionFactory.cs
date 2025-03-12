using RuleBasedFilterLibrary.Core.Model.Comparison;
using RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Generic;
using RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.GenericInterval;
using RuleBasedFilterLibrary.Core.Model.Expressions;

namespace RuleBasedFilterLibrary.Core.Services.Expression;

public class GenericExpressionFactory<Type>
{
    private static readonly Dictionary<string, Func<string[], IExpression>> _intExpressions = new()
    {
        {"==", args => new EqualityExpression<Type>(new DefaultComparer(), (Type)Convert.ChangeType(args[0], typeof(Type))) },
        {"!=", args => new NotEqualExpression<Type>(new DefaultComparer(), (Type)Convert.ChangeType(args[0], typeof(Type))) },
        {">", args => new GreaterThanExpression<Type>(new DefaultComparer(), (Type)Convert.ChangeType(args[0], typeof(Type))) },
        {">=", args => new GreaterOrEqualThanExpression<Type>(new DefaultComparer(), (Type)Convert.ChangeType(args[0], typeof(Type))) },
        {"<", args => new LessThanExpression<Type>(new DefaultComparer(), (Type)Convert.ChangeType(args[0], typeof(Type))) },
        {"<=", args => new LessOrEqualThanExpression<Type>(new DefaultComparer(), (Type)Convert.ChangeType(args[0], typeof(Type))) },
        {"between", args => new IntervalExpression<Type>(new DefaultComparer(), (Type)Convert.ChangeType(args[0], typeof(Type)), (Type)Convert.ChangeType(args[1], typeof(Type))) },
    };

    public static IExpression Create(string comparisonExpression)
    {
        var comparisonExpressionMembers = comparisonExpression.Split(' ');
        var comparisonOperator = comparisonExpressionMembers[0];
        var ethalonValues = comparisonExpressionMembers.Skip(1).ToArray();

        if (_intExpressions.TryGetValue(comparisonOperator, out var creator))
        {
            return creator(ethalonValues);
        }

        throw new ArgumentException($"Type {typeof(Type)} does not support operation {comparisonOperator}");
    }
}
