using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;
using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;
using RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;
using RuleBasedFilterLibrary.Extensions;
using RuleBasedFilterLibrary.Infrastructure.Services.RequestsStorage;
using System.Numerics;

namespace TestTileApi.CustomSequenceAnalyzers;

public class NonRandomSequenceAnalyzer(IRequestStorage requestStorage, RuleBasedRequestFilterOptions options) : IRequestSequenceAnalyzer
{
    private static readonly double _maxDistance = 40.0;

    public async Task<bool> Analyze(string userIp, List<ParameterSequenceAnalysis> parameterRules)
    {
        var isParameterRequestedRandomly = await IsParameterRequestedRandomly(userIp, parameterRules);

        if (!isParameterRequestedRandomly)
            return false;

        return true;
    }

    private async Task<bool> IsParameterRequestedRandomly(string userIp, List<ParameterSequenceAnalysis> parameterRules)
    {
        var requests = await requestStorage.GetRequestsOfUserAsync(userIp);

        for (var i = 1; i < requests.Count; i++)
        {
            var currentRequestParametersValues = requests[i].Parameters.Values.Select(int.Parse).ToList();
            var previousRequestParametersValues = requests[i - 1].Parameters.Values.Select(int.Parse).ToList();

            var distance = CalculateEuclideanDistance(currentRequestParametersValues, previousRequestParametersValues);
            if (distance > _maxDistance)
                return false;
        }

        return true;
    }

    private static double CalculateEuclideanDistance(IList<int> array1, IList<int> array2)
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
