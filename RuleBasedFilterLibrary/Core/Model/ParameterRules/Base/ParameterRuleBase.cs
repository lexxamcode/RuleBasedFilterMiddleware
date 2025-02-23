namespace RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;

public abstract class ParameterRuleBase
{
    public required string Name { get; set; }
    public abstract bool CompareTo(string actualParameterValue);
}
