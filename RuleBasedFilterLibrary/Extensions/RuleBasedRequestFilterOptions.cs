﻿namespace RuleBasedFilterLibrary.Extensions;

public class RuleBasedRequestFilterOptions
{
    public string ConfigurationFileName { get; set; } = "rulesConf.yml";
    public bool EnableRequestSequenceValidation { get; set; } = false;
    public string NodeAddress { get; set; } = "http://localhost:9200";
    public string IndexName { get; set; } = "requests";
    public int MaxNumberOfRecentRequests { get; set; } = 100;
    public int MinLengthOfAnalyzedSequence { get; set; } = 50;
    public static RuleBasedRequestFilterOptions Default => new();
}