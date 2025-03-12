using OpenSearch.Client;
using RuleBasedFilterLibrary.Core.Model.Requests;
using RuleBasedFilterLibrary.Infrastructure.Services.RequestStorage;

namespace RuleBasedFilterLibrary.Core.Services.RequestStorageManager;

public class RequestStorageManager(IRequestStorage requestStorage) : IRequestStorageManager
{
    public async Task AddAsync(Request request)
    {
        await requestStorage.AddAsync(request);
    }

    public async Task<List<Request>> GetRequestsOfUserAsync(string userIp)
    {
        return await requestStorage.GetRequestsOfUserAsync(userIp);
    }
}
