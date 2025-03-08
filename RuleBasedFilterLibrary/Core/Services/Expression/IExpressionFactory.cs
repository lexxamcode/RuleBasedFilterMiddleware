using RuleBasedFilterLibrary.Core.Model.Expressions;

namespace RuleBasedFilterLibrary.Core.Services.Expression;

public interface IExpressionFactory
{
    public IExpression Create(string type, string comparisonExpression);
}
