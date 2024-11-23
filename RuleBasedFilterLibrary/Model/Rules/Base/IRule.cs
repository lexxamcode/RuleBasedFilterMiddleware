using Microsoft.AspNetCore.Http;

namespace RuleBasedFilterLibrary.Model.Rules.Base;

public interface IRule
{
    public Task<bool> IsRequestValid(HttpRequest request);
}