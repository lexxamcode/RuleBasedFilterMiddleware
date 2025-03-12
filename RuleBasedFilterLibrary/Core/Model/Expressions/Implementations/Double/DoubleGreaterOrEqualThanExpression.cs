﻿using System.Globalization;

namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Double;

public class DoubleGreaterOrEqualThanExpression(double ethalonValue) : IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualValueAsDouble = double.Parse(actualValue, CultureInfo.InvariantCulture);
        return actualValueAsDouble >= ethalonValue;
    }
}
