using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;
using RuleBasedFilterLibrary.Core.Utils;
using RuleBasedFilterLibrary.Infrastructure.Model.RawSequenceAnalyses;

namespace RuleBasedFilterLibrary.Core.Services.ParameterSequenceAnalysisFactory;

public class ParameterSequenceAnalysisFactory : IParamterSequenceAnalysisFactory
{
    public ParameterSequenceAnalysis CreateFromRawParameterSequenceAnalysis(RawSequenceAnalysisParameter rawParameterSequenceAnalysis)
    {
        var parameterSequenceAnalysis = new ParameterSequenceAnalysis
        {
            Name = rawParameterSequenceAnalysis.Name,
            Type = TypeResolver.GetTypeFromString(rawParameterSequenceAnalysis.Type)
        };

        return parameterSequenceAnalysis;
    }
}
