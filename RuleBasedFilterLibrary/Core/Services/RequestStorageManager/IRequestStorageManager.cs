using OpenSearch.Client;
using RuleBasedFilterLibrary.Core.Model.Requests;

namespace RuleBasedFilterLibrary.Core.Services.RequestStorageManager;

public interface IRequestStorageManager
{
    public Task<List<Request>> GetRequestsOfUserAsync(string userIp);
    public Task<IndexResponse> AddAsync(Request request);
}
