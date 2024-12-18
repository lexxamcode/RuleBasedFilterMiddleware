using Microsoft.AspNetCore.Http;
using RuleBasedFilterLibrary.Extensions;
using RuleBasedFilterLibrary.Model.Requests;
using RuleBasedFilterLibrary.Model.Rules.Base;
using RuleBasedFilterLibrary.Model.Rules.Base.ParameterComparison;
using RuleBasedFilterLibrary.Services;
using RuleBasedFilterLibrary.Services.DeepAnalysis;
using System.Web;

namespace RuleBasedFilterLibrary.Middleware;

public class RuleBasedRequestFilterMiddleware(
    RequestDelegate next,
    IRulesLoaderService rulesLoaderService,
    IRequestSequenceAnalyzer requestSequenceAnalyzer,
    RuleBasedRequestFilterOptions options)
{
    private readonly RequestDelegate _next = next;
    private readonly List<RequestRule> _rules = rulesLoaderService.LoadRulesFromConfigurationFile(options.ConfigurationFileName);

    public async Task InvokeAsync(HttpContext context)
    {
        var isRequestValid = await ValidateRequest(context);

        if (!isRequestValid)
        {
            context.Response.StatusCode = 403;
            return;
        }

        await _next(context);
    }

    public async Task<bool> ValidateRequest(HttpContext context)
    {
        var request = ConstructRequestFromContext(context);

        if (options.EnableRequestSequenceValidation)
            await requestSequenceAnalyzer.IndexRequestAsync(request);

        foreach (var rule in _rules)
        {
            var isRequestValid = await rule.IsRequestValid(context);
            if (!isRequestValid)
                return false;

            if (options.EnableRequestSequenceValidation)
            {
                var brokenRulesCount = 0;

                foreach (var parameterRule in rule.ParameterRules)
                {
                    if (parameterRule.ComparisonType is not ComparisonType.NonMonotous &&
                        parameterRule.ComparisonType is not ComparisonType.Monotonous)
                        continue;

                    var isParameterValid = await requestSequenceAnalyzer.Validate(request, parameterRule);
                    if (!isParameterValid)
                        brokenRulesCount++;
                }

                if (brokenRulesCount == rule.ParameterRules.Count)
                    return false;
            }
        }

        return true;
    }

    public static Request ConstructRequestFromContext(HttpContext context)
    {
        var userIp = context.Connection.RemoteIpAddress.ToString();
        var requestTime = DateTime.UtcNow;
        var parameters = HttpUtility.ParseQueryString(context.Request.QueryString.Value) ?? [];

        var request = new Request
        {
            UserIp = userIp,
            RequestTime = requestTime,
            Parameters = parameters.ToDictionary()
        };

        return request;
    }
}
