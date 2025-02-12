using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;
using RuleBasedFilterLibrary.Infrastructure.Model.RawSequenceAnalyses;

namespace RuleBasedFilterLibrary.Core.Services.SequenceAnalysisFactory;

public interface ISequenceAnalysisFactory
{
    public SequenceAnalysis CreateFromRawSequenceAnalysis(RawSequenceAnalysis rawSequenceAnalysis);
}
