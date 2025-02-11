using Microsoft.AspNetCore.Http;
using RuleBasedFilterLibrary.Extensions;
using RuleBasedFilterLibrary.Model.Requests;
using RuleBasedFilterLibrary.Model.Rules.Base;
using RuleBasedFilterLibrary.Model.Rules.Base.ParameterComparison;
using RuleBasedFilterLibrary.Services.DeepAnalysis;
using RuleBasedFilterLibrary.Services.RequestValidation;
using System.Web;

namespace RuleBasedFilterLibrary.Services.Requestvalidation;

public class RequestValidationService(
    IRulesLoaderService rulesLoaderService,
    IRequestSequenceAnalyzer requestSequenceAnalyzer,
    RuleBasedRequestFilterOptions options) : IRequestValidationService
{
    private readonly List<RequestRule> _rules = rulesLoaderService.LoadRulesFromConfigurationFile(options.ConfigurationFileName);

    public async Task<bool> IsValidAsync(HttpContext context)
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
