using Microsoft.AspNetCore.Http;
using RuleBasedFilterLibrary.Services;
using YmlRulesFileParser.Model.Rules.Base;

namespace RuleBasedFilterLibrary.Middleware;

public class RuleBasedRequestFilterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly List<RequestRule> _rules;

    public RuleBasedRequestFilterMiddleware(RequestDelegate next, IRulesLoaderService rulesLoaderService, string configurationFilename)
    {
        _next = next;
        _rules = rulesLoaderService.LoadRulesFromConfigurationFile(configurationFilename);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
    }
}
