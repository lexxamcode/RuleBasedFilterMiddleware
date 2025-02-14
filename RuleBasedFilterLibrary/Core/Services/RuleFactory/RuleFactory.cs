using RuleBasedFilterLibrary.Core.Model.Rules;
using RuleBasedFilterLibrary.Core.Services.ParameterRuleFactory;
using RuleBasedFilterLibrary.Core.Services.SequenceAnalysisFactory;
using RuleBasedFilterLibrary.Core.Utils;
using RuleBasedFilterLibrary.Infrastructure.Model.RawRules;

namespace RuleBasedFilterLibrary.Core.Services.RuleFactory;

public class RuleFactory(IParameterRuleFactory parameterRuleFactory, ISequenceAnalysisFactory sequenceAnalysisFactory) : IRuleFactory
{
    public IRule CreateRuleFromItsRawRepresentation(RawRule rawRequestRule)
    {
        var accessPolicy = AccessPolicyFactory.CreateAccessPolicyFromString(rawRequestRule.AccessPolicy);
        var requestParameterRules = rawRequestRule.ParameterRules
            .Select(parameterRuleFactory.CreateFromRawRequestParameterRule)
            .ToList();

        var sequenceAnalyses = rawRequestRule.SequenceAnalyses
            .Select(sequenceAnalysisFactory.CreateFromRawSequenceAnalysis)
            .ToList();

        var rule = new Rule
        {
            Name = rawRequestRule.Name,
            SourceIp = rawRequestRule.SourceIp,
            AccessPolicy = accessPolicy,
            HttpMethod = rawRequestRule.Method,
            Endpoint = rawRequestRule.Endpoint,
            ParameterRules = requestParameterRules,
            SequenceAnalyses = sequenceAnalyses
        };

        return rule;
    }
}