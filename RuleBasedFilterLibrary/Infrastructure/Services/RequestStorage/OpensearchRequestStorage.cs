using OpenSearch.Client;
using RuleBasedFilterLibrary.Core.Model.Requests;
using RuleBasedFilterLibrary.Extensions;

namespace RuleBasedFilterLibrary.Infrastructure.Services.RequestStorage;

public class OpensearchRequestStorage : IRequestStorage
{
    private readonly OpenSearchClient _openSearchClient;
    private readonly RuleBasedRequestFilterOptions _options;

    public OpensearchRequestStorage(RuleBasedRequestFilterOptions options)
    {
        var nodeAddress = new Uri(options.NodeAddress);
        var config = new ConnectionSettings(nodeAddress).DefaultIndex(options.IndexName);

        _openSearchClient = new OpenSearchClient(config);
        _options = options;

        var deleteRequest = new DeleteIndexRequest(Indices.Parse(options.IndexName));
        _openSearchClient.Indices.Delete(deleteRequest);
    }

    public async Task<List<Request>> GetRequestsOfUserAsync(string userIp)
    {
        var lastRequestsFromCurrentIp = await _openSearchClient.SearchAsync<Request>(req => req
                                            .Index(_options.IndexName)
                                            .Query(query => query
                                                .Match(match => match
                                                    .Field(field => field.UserIp)
                                                    .Query(userIp)))
                                            .Sort(sort => sort
                                                .Descending(fieldName => fieldName.RequestTime))
                                            .Size(_options.MaxNumberOfRecentRequests));

        var lastRequestsFromCurrentIpAsList = lastRequestsFromCurrentIp.Documents.ToList();
        return lastRequestsFromCurrentIpAsList;
    }

    public async Task AddAsync(Request request)
    {
        await _openSearchClient.IndexAsync(new IndexRequest<Request>(request, _options.IndexName));
    }
}
