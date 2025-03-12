namespace RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;

public interface ISequenceAnalysis
{
    public Task<bool> DidAnalysisSucceed(string userIp);
}