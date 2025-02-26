namespace RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;

public interface IParameterRule
{
    public string Name { get; set; }
    public bool Validate(Dictionary<string, string> arguments);
}
