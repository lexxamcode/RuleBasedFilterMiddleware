using RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;

namespace RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;

public class SequenceAnalysis(IRequestSequenceAnalyzer analyzer)
{
    public List<ParameterSequenceAnalysis> Parameters { get; set; } = [];

    public async Task<bool> Analyse(string userIp)
    {
        return await analyzer.Analyze(userIp, Parameters);
    }
}
