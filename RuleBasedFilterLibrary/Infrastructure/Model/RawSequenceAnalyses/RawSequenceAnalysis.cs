namespace RuleBasedFilterLibrary.Infrastructure.Model.RawSequenceAnalyses;

public class RawSequenceAnalysis
{
    public string AnalysisType { get; set; } = string.Empty;
    public List<RawSequenceAnalysisParameter> ByArguments { get; set; } = [];
}
