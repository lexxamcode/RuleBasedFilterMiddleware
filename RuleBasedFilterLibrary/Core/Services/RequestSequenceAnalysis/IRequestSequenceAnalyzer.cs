using RuleBasedFilterLibrary.Core.Model.ParameterRules;

namespace RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;

public interface IRequestSequenceAnalyzer
{
    public Task<bool> Analyze(string userIp, List<ParameterRule> parameterRules);
}
