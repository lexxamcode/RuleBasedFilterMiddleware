using OpenSearch.Client;
using RuleBasedFilterLibrary.Core.Model.Requests;

namespace RuleBasedFilterLibrary.Infrastructure.Services.RequestsStorage;

public interface IRequestStorage
{
    public Task<List<Request>> GetRequestsOfUserAsync(string userIp);
    public Task<IndexResponse> AddAsync(Request request);
}
