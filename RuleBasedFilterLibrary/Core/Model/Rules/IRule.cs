using Microsoft.AspNetCore.Http;

namespace RuleBasedFilterLibrary.Core.Model.Rules;

public interface IRule
{
    public Task<bool> IsRequestValid(HttpContext context);
    public Task<bool> IsRequestSequenceValid(HttpContext context);
}