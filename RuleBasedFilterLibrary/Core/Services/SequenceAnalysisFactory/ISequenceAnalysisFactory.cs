using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;
using RuleBasedFilterLibrary.Infrastructure.Model.RawSequenceAnalyses;

namespace RuleBasedFilterLibrary.Core.Services.SequenceAnalysisFactory;

public interface ISequenceAnalysisFactory
{
    public ISequenceAnalysis CreateFromRawSequenceAnalysis(RawSequenceAnalysis rawSequenceAnalysis);
}