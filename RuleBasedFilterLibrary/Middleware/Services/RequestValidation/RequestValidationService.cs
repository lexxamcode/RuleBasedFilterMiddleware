using Microsoft.AspNetCore.Http;
using RuleBasedFilterLibrary.Core.Model.Requests;
using RuleBasedFilterLibrary.Core.Model.Rules;
using RuleBasedFilterLibrary.Core.Services.RequestStorageManager;
using RuleBasedFilterLibrary.Extensions;
using RuleBasedFilterLibrary.Infrastructure.Services.RulesFileParsing;
using System.Web;

namespace RuleBasedFilterLibrary.Middleware.Services.RequestValidation;

public class RequestValidationService : IRequestValidationService
{
    private readonly List<IRule> _rules = [];
    private readonly IRulesLoader _rulesLoader;
    private readonly IRequestStorageManager? _requestStorageManager;
    private readonly RuleBasedRequestFilterOptions _options;

    public RequestValidationService(IRulesLoader rulesLoader, RuleBasedRequestFilterOptions options)
    {
        _rulesLoader = rulesLoader;
        _rules = _rulesLoader.LoadRulesFromConfigurationFile(options.ConfigurationFileName);
        _options = options;
    }

    public RequestValidationService(IRulesLoader rulesLoader, IRequestStorageManager requestStorageManager,
        RuleBasedRequestFilterOptions options)
    {
        _rulesLoader = rulesLoader;
        _rules = _rulesLoader.LoadRulesFromConfigurationFile(options.ConfigurationFileName);
        _options = options;

        _requestStorageManager = requestStorageManager;
    }

    public async Task<bool> IsValidAsync(HttpContext context)
    {
        var request = ConstructRequestFromContext(context);

        if (_options.EnableRequestSequenceValidation && _requestStorageManager is not null)
            await _requestStorageManager.AddAsync(request);

        foreach (var rule in _rules)
        {
            var isRequestValid = await rule.IsRequestValid(context);
            if (!isRequestValid)
                return false;

            if (!_options.EnableRequestSequenceValidation)
                continue;

            var isRequestSequenceValid = await rule.IsRequestSequenceValid(context);
            if (!isRequestSequenceValid)
                return false;
        }

        return true;
    }

    private static Request ConstructRequestFromContext(HttpContext context)
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
