namespace RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;

public interface IParameterRule
{
    public string Name { get; set; }
    public bool CompareTo(string actualParameterValue);
}
