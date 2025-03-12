using RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;

namespace RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;

public class SequenceAnalysis(IRequestSequenceAnalyzer analyzer) : ISequenceAnalysis
{
    public List<ParameterSequenceAnalysis> Parameters { get; set; } = [];

    public async Task<bool> DidAnalysisSucceed(string userIp)
    {
        return await analyzer.DidAnalysisSucceed(userIp, Parameters);
    }
}
