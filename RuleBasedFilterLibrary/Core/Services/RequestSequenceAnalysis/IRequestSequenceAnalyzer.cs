using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;

namespace RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;

public interface IRequestSequenceAnalyzer
{
    public Task<bool> DidAnalysisSucceed(string userIp, List<ParameterSequenceAnalysis> parameterRules);
}
