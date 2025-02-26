using RuleBasedFilterLibrary.Core.Model.Expressions;
using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;

namespace RuleBasedFilterLibrary.Core.Model.ParameterRules;

public class LongitudeParameterRule(IExpression expression) : ParameterRuleBase
{
    public override bool Validate(Dictionary<string, string> arguments)
    {
        var actualValue = int.Parse(arguments[ArgumentName]);
        var zoom = int.Parse(arguments["z"]);
        var actualTileLongitude = TileXToLong(actualValue, zoom);

        return expression.MatchesExpression(actualTileLongitude.ToString());
    }

    private static double TileXToLong(int tileIndex, int zoom)
    {
        return tileIndex / (double)(1 << zoom) * 360.0 - 180;
    }
}
