using Microsoft.AspNetCore.Http;
using RuleBasedFilterLibrary.Services;
using YmlRulesFileParser.Model.Rules.Base;

namespace RuleBasedFilterLibrary.Middleware;

public class RuleBasedRequestFilterMiddleware(
    RequestDelegate next,
    IRulesLoaderService rulesLoaderService,
    string configurationFilename)
{
    private readonly RequestDelegate _next = next;
    private readonly List<RequestRule> _rules = rulesLoaderService.LoadRulesFromConfigurationFile(configurationFilename);

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;

        foreach (var rule in _rules)
        {
            var validationResult = await rule.IsRequestValid(request);
            if (!validationResult)
            {
                context.Response.StatusCode = 403;
                return;
            }
        }
        await _next(context);
    }
}
