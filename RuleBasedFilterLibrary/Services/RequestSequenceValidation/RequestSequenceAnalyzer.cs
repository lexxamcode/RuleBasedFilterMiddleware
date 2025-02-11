using OpenSearch.Client;
using RuleBasedFilterLibrary.Model.Requests;
using RuleBasedFilterLibrary.Model.Rules.Base;

namespace RuleBasedFilterLibrary.Services.DeepAnalysis;

public class RequestSequenceAnalyzer(IOpenSearchClient openSearchClient) : IRequestSequenceAnalyzer
{
    public async Task<bool> Validate(Request request, RequestParameterRule requestParameterRule)
    {
        var lastRequestsFromCurrentIp = await openSearchClient.SearchAsync<Request>(req => req
                                            .Index("requests")
                                            .Query(query => query
                                                .Match(match => match
                                                    .Field(field => field.UserIp)
                                                    .Query(request.UserIp)))
                                            .Sort(sort => sort
                                                .Descending(fieldName => fieldName.RequestTime))
                                            .Size(100));
        var lastRequestsFromCurrentIpAsList = lastRequestsFromCurrentIp.Documents.ToList();

        var isParameterMonotone = IsParameterMonotone(lastRequestsFromCurrentIpAsList, requestParameterRule);
        return !isParameterMonotone;
    }

    private static bool IsParameterMonotone(List<Request> requestsListSortedByTime, RequestParameterRule requestParameterRule)
    {
        if (requestsListSortedByTime.Count <= 50)
            return false;

        var hasIncreased = false;
        var hasDecreased = false;

        for (var i = 1; i < requestsListSortedByTime.Count; i++)
        {
            var currentRequest = requestsListSortedByTime[i];
            var currentParameterValueAsString = currentRequest.Parameters[requestParameterRule.Name];
            var currentParameterValueCastedToItsType = Convert.ChangeType(currentParameterValueAsString, requestParameterRule.ParameterType);

            var previousRequest = requestsListSortedByTime[i - 1];
            var previousParameterValueAsString = previousRequest.Parameters[requestParameterRule.Name];
            var previousParameterValueCastedToItsType = Convert.ChangeType(previousParameterValueAsString, requestParameterRule.ParameterType);

            if (CastToIComparableAndCompare(currentParameterValueCastedToItsType, previousParameterValueCastedToItsType) > 0)
                hasIncreased = true;

            if (CastToIComparableAndCompare(currentParameterValueCastedToItsType, previousParameterValueCastedToItsType) < 0)
                hasDecreased = true;
        }

        return !(hasIncreased && hasDecreased);
    }

    public async Task<IndexResponse> IndexRequestAsync(Request request)
    {
        try
        {
            var indexResponse = await openSearchClient.IndexAsync(new IndexRequest<Request>(request, "requests"));
            return indexResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    private static int CastToIComparableAndCompare(object? actualValue, object? ethalonValue)
    {
        if (actualValue is not IComparable actualValueAsComparable ||
            ethalonValue is not IComparable ethalonValueAsComparable)
            throw new ArgumentException("actualValue or ethalonValue are not IComparable");

        return actualValueAsComparable.CompareTo(ethalonValueAsComparable);
    }
}
