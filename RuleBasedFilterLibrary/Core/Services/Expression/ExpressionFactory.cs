using RuleBasedFilterLibrary.Core.Model.Expressions;
using RuleBasedFilterLibrary.Core.Services.Expression.DoubleExpressions;
using RuleBasedFilterLibrary.Core.Services.Expression.FloatExpressions;
using RuleBasedFilterLibrary.Core.Services.Expression.StringExpressions;

namespace RuleBasedFilterLibrary.Core.Services.Expression;

public class ExpressionFactory : IExpressionFactory
{
    private readonly Dictionary<string, Func<string, IExpression>> _expressions = new()
    {
        {"int", comparisonExpression => GenericExpressionFactory<int>.Create(comparisonExpression) },
        {"bool", comparisonExpression => GenericExpressionFactory<bool>.Create(comparisonExpression) },
        {"char", comparisonExpression => GenericExpressionFactory<char>.Create(comparisonExpression) },
        {"byte", comparisonExpression => GenericExpressionFactory<byte>.Create(comparisonExpression) },
        {"short", comparisonExpression => GenericExpressionFactory<short>.Create(comparisonExpression) },
        {"long", comparisonExpression => GenericExpressionFactory<long>.Create(comparisonExpression) },
        {"double", comparisonExpression => DoubleExpressionFactory.Create(comparisonExpression) },
        {"float", comparisonExpression => FloatExpressionFactory.Create(comparisonExpression) },
        {"string", comparisonExpression => StringExpressionFactory.Create(comparisonExpression) },
    };

    public IExpression Create(string type, string comparisonExpression)
    {
        if (_expressions.TryGetValue(type, out var creator))
            return creator(comparisonExpression);

        throw new ArgumentException($"Type {type} is not supported");
    }
}