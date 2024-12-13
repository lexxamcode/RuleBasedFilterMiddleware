namespace RuleBasedFilterLibrary.Extensions;

public class RuleBasedRequestFilterOptions
{
    public string ConfigurationFileName { get; set; } = "rulesConf.yml";
    public bool EnableRequestSequenceValidation { get; set; } = false;
}
