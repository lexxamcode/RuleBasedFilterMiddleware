﻿using System.Globalization;

namespace RuleBasedFilterLibrary.Core.Model.Expressions.Implementations.Float;

public class FloatLessThanExpression(float ethalonValue) : FloatExpressionBase, IExpression
{
    public bool MatchesExpression(string actualValue)
    {
        var actualValueAsFloat = float.Parse(actualValue, CultureInfo.InvariantCulture);
        return actualValueAsFloat < ethalonValue;
    }
}

