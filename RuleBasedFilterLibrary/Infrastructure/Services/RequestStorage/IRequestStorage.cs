using RuleBasedFilterLibrary.Core.Model.Requests;

namespace RuleBasedFilterLibrary.Infrastructure.Services.RequestStorage;

public interface IRequestStorage
{
    public Task<List<Request>> GetRequestsOfUserAsync(string userIp);
    public Task AddAsync(Request request);
}
