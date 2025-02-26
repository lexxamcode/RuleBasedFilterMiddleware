using RuleBasedFilterLibrary.Core.Model.Expressions;
using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;

namespace RuleBasedFilterLibrary.Core.Model.ParameterRules;

public class LatitudeParameterRule(IExpression expression) : ParameterRuleBase
{
    public override bool Validate(Dictionary<string, string> arguments)
    {
        var actualValue = int.Parse(arguments[ArgumentName]);
        var zoom = int.Parse(arguments["z"]);
        var latitude = TileYToLat(actualValue, zoom);

        return expression.MatchesExpression(latitude.ToString());
    }    

    private static double TileYToLat(int y, int z)
    {
        var n = Math.PI - 2.0 * Math.PI * y / (1 << z);
        return 180.0 / Math.PI * Math.Atan(0.5 * (Math.Exp(n) - Math.Exp(-n)));
    }
}
