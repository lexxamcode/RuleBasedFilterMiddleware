using Microsoft.AspNetCore.Http;

namespace YmlRulesFileParser.Model.Rules.Base;

public interface IRule
{
    public Task<bool> IsRequestValid(HttpRequest request);
}
