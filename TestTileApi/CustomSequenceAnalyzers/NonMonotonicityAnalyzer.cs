﻿using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;
using RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;
using RuleBasedFilterLibrary.Extensions;
using RuleBasedFilterLibrary.Infrastructure.Services.RequestStorage;

namespace TestTileApi.CustomSequenceAnalyzers;

public class NonMonotonicityAnalyzer(IRequestStorage requestStorage, RuleBasedRequestFilterOptions options) : IRequestSequenceAnalyzer
{
    public async Task<bool> DidAnalysisSucceed(string userIp, List<ParameterSequenceAnalysis> parameterRules)
    {
        var brokenRulesCount = 0;

        foreach (var parameterRule in parameterRules)
        {
            var requests = await requestStorage.GetRequestsOfUserAsync(userIp);
            var isParameterRequestedMonotonously = await IsParameterRequestedMonotonously(userIp, parameterRule);
            if (isParameterRequestedMonotonously)
                brokenRulesCount++;
        }

        if (brokenRulesCount == parameterRules.Count)
            return false;

        return true;
    }

    private async Task<bool> IsParameterRequestedMonotonously(string userIp, ParameterSequenceAnalysis requestParameterRule)
    {
        var requests = await requestStorage.GetRequestsOfUserAsync(userIp);

        if (requests.Count <= options.MinLengthOfAnalyzedSequence)
            return false;

        var hasIncreased = false;
        var hasDecreased = false;

        for (var i = 1; i < requests.Count; i++)
        {
            var currentRequest = requests[i];
            var currentParameterValueAsString = currentRequest.Parameters[requestParameterRule.Name];
            var currentParameterValueCastedToItsType = Convert.ChangeType(currentParameterValueAsString, requestParameterRule.Type);

            var previousRequest = requests[i - 1];
            var previousParameterValueAsString = previousRequest.Parameters[requestParameterRule.Name];
            var previousParameterValueCastedToItsType = Convert.ChangeType(previousParameterValueAsString, requestParameterRule.Type);

            if (CastToIComparableAndCompare(currentParameterValueCastedToItsType, previousParameterValueCastedToItsType) > 0)
                hasIncreased = true;

            if (CastToIComparableAndCompare(currentParameterValueCastedToItsType, previousParameterValueCastedToItsType) < 0)
                hasDecreased = true;
        }

        return !(hasIncreased && hasDecreased);
    }

    private static int CastToIComparableAndCompare(object? actualValue, object? ethalonValue)
    {
        if (actualValue is not IComparable actualValueAsComparable ||
            ethalonValue is not IComparable ethalonValueAsComparable)
            throw new ArgumentException("actualValue or ethalonValue are not IComparable");

        return actualValueAsComparable.CompareTo(ethalonValueAsComparable);
    }
}
