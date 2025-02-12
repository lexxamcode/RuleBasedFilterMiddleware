using RuleBasedFilterLibrary.Core.Model.ParameterRules;
using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;

namespace RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;

public interface IRequestSequenceAnalyzer
{
    public Task<bool> Analyze(string userIp, List<ParameterSequenceAnalysis> parameterRules);
}
