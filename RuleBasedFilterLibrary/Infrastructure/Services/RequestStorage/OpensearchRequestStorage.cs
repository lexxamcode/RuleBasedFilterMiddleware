using OpenSearch.Client;
using RuleBasedFilterLibrary.Core.Model.Requests;
using RuleBasedFilterLibrary.Extensions;

namespace RuleBasedFilterLibrary.Infrastructure.Services.RequestsStorage;

public class OpensearchRequestStorage(IOpenSearchClient openSearchClient, RuleBasedRequestFilterOptions options) : IRequestStorage
{
    public async Task<List<Request>> GetRequestsOfUserAsync(string userIp)
    {
        var lastRequestsFromCurrentIp = await openSearchClient.SearchAsync<Request>(req => req
                                            .Index(options.IndexName)
                                            .Query(query => query
                                                .Match(match => match
                                                    .Field(field => field.UserIp)
                                                    .Query(userIp)))
                                            .Sort(sort => sort
                                                .Descending(fieldName => fieldName.RequestTime))
                                            .Size(options.MaxNumberOfRecentRequests));

        var lastRequestsFromCurrentIpAsList = lastRequestsFromCurrentIp.Documents.ToList();
        return lastRequestsFromCurrentIpAsList;
    }

    public async Task<IndexResponse> AddAsync(Request request)
    {
        var indexResponse = await openSearchClient.IndexAsync(new IndexRequest<Request>(request, options.IndexName));
        return indexResponse;
    }
}
