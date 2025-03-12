using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;
using RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;
using RuleBasedFilterLibrary.Extensions;
using RuleBasedFilterLibrary.Infrastructure.Services.RequestStorage;

namespace TestTileApi.CustomSequenceAnalyzers;

public class NonRandomSequenceAnalyzer(IRequestStorage requestStorage, RuleBasedRequestFilterOptions options) : IRequestSequenceAnalyzer
{
    private static readonly double _maxDistance = 40.0;

    public async Task<bool> DidAnalysisSucceed(string userIp, List<ParameterSequenceAnalysis> parameterRules)
    {
        var isParameterRequestedRandomly = await IsParameterRequestedRandomly(userIp, parameterRules);

        if (!isParameterRequestedRandomly)
            return true;

        return false;
    }

    private async Task<bool> IsParameterRequestedRandomly(string userIp, List<ParameterSequenceAnalysis> parameterRules)
    {
        var parameterNamesToCheckRandom = parameterRules.Select(rule => rule.Name).ToHashSet();

        var requests = await requestStorage.GetRequestsOfUserAsync(userIp);

        if (requests.Count < options.MinLengthOfAnalyzedSequence)
            return false;

        for (var i = 1; i < requests.Count; i++)
        {
            var currentRequestParametersValues = requests[i]
                .Parameters
                .Where(parameter => parameterNamesToCheckRandom
                .Contains(parameter.Key))
                .Select(kvp => int.Parse(kvp.Value))
                .ToList();

            var previousRequestParametersValues = requests[i - 1]
                .Parameters
                .Where(parameter => parameterNamesToCheckRandom
                .Contains(parameter.Key))
                .Select(kvp => int.Parse(kvp.Value))
                .ToList();

            var distance = CalculateEuclideanDistance(currentRequestParametersValues, previousRequestParametersValues);
            if (distance > _maxDistance)
                return true;
        }

        return false;
    }

    private static double CalculateEuclideanDistance(List<int> array1, List<int> array2)
    {
        if (array1.Count != array2.Count)
        {
            throw new ArgumentException("Массивы должны иметь одинаковую длину");
        }

        var sum = 0.0;
        for (int i = 0; i < array1.Count; i++)
        {
            var diff = array1[i] - array2[i];
            sum += diff * diff;
        }

        return Math.Sqrt(sum);
    }
}
