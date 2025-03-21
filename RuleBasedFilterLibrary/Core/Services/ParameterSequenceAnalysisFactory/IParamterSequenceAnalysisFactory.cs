﻿using RuleBasedFilterLibrary.Core.Model.ParameterRules;
using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;
using RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;
using RuleBasedFilterLibrary.Infrastructure.Model.RawSequenceAnalyses;

namespace RuleBasedFilterLibrary.Core.Services.ParameterSequenceAnalysisFactory;

public interface IParamterSequenceAnalysisFactory
{
    public ParameterSequenceAnalysis CreateFromRawParameterSequenceAnalysis(RawSequenceAnalysisParameter rawParameterSequenceAnalysis);
}
