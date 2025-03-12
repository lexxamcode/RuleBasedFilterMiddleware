using Microsoft.Extensions.DependencyInjection;
using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;
using RuleBasedFilterLibrary.Core.Services.ParameterSequenceAnalysisFactory;
using RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;
using RuleBasedFilterLibrary.Infrastructure.Model.RawSequenceAnalyses;

namespace RuleBasedFilterLibrary.Core.Services.SequenceAnalysisFactory;

public class SequenceAnalysisFactory(IServiceProvider serviceProvider, IParamterSequenceAnalysisFactory paramterSequenceAnalysisFactory): ISequenceAnalysisFactory
{
    public ISequenceAnalysis CreateFromRawSequenceAnalysis(RawSequenceAnalysis rawSequenceAnalysis)
    {
        var sequenceAnalysers = serviceProvider.GetServices<IRequestSequenceAnalyzer>();
        var sequenceAnalyzer = sequenceAnalysers.Single(analyzer => analyzer.GetType().Name == rawSequenceAnalysis.AnalysisType);

        var sequenceAnalysis = new SequenceAnalysis(sequenceAnalyzer)
        {
            Parameters = rawSequenceAnalysis.ByArguments.Select(paramterSequenceAnalysisFactory.CreateFromRawParameterSequenceAnalysis).ToList()
        };

        return sequenceAnalysis;
    }
}
