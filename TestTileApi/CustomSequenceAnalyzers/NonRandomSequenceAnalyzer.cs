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

        var meanDistance = 0d;
        for (var i = 1; i < requests.Count; i++)
        {
            var currentRequestParametersValues = requests[i]
                .Parameters
                .Where(parameter => parameterNamesToCheckRandom
                .Contains(parameter.Key))
                .ToDictionary();

            var previousRequestParametersValues = requests[i - 1]
                .Parameters
                .Where(parameter => parameterNamesToCheckRandom
                .Contains(parameter.Key))
                .ToDictionary();

            var distance = CalculateEuclideanDistance(currentRequestParametersValues, previousRequestParametersValues);
            meanDistance += distance;

            if (distance > _maxDistance)
                return true;
        }
        meanDistance /= requests.Count;

        return false;
    }

    private static double CalculateEuclideanDistance(IDictionary<string, string> firstCoordinates, IDictionary<string, string> secondCoordinates)
    {
        var firstZ = int.Parse(firstCoordinates["z"]);
        var firstX = int.Parse(firstCoordinates["x"]);
        var firstY = int.Parse(firstCoordinates["y"]);

        var secondZ = int.Parse(secondCoordinates["z"]);
        var secondX = int.Parse(secondCoordinates["x"]);
        var secondY = int.Parse(secondCoordinates["y"]);

        if (firstZ != secondZ)
            return 0;

        var distance = Math.Sqrt(Math.Pow(secondX - firstX, 2) + Math.Pow(secondY - firstY, 2));
        return distance;
    }
}
