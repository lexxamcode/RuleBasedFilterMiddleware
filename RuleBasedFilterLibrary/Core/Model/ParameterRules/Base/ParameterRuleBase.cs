namespace RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;

public abstract class ParameterRuleBase : IParameterRule
{
    public required string Name { get; set; }
    public required string ArgumentName { get; set; }
    public abstract bool Validate(Dictionary<string, string> arguments);
}
