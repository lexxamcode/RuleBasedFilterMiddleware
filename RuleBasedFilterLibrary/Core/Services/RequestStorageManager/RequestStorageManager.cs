using OpenSearch.Client;
using RuleBasedFilterLibrary.Core.Model.Requests;
using RuleBasedFilterLibrary.Infrastructure.Services.RequestsStorage;

namespace RuleBasedFilterLibrary.Core.Services.RequestStorageManager;

public class RequestStorageManager(IRequestStorage requestStorage) : IRequestStorageManager
{
    public async Task<IndexResponse> AddAsync(Request request)
    {
        return await requestStorage.AddAsync(request);
    }

    public async Task<List<Request>> GetRequestsOfUserAsync(string userIp)
    {
        return await requestStorage.GetRequestsOfUserAsync(userIp);
    }
}
