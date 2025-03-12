using Microsoft.AspNetCore.Http;
using RuleBasedFilterLibrary.Core.Model.AccessPolicies;
using RuleBasedFilterLibrary.Core.Model.ParameterRules.Base;
using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;
using RuleBasedFilterLibrary.Extensions;
using System.Web;

namespace RuleBasedFilterLibrary.Core.Model.Rules;

/// <summary>
/// Базовый класс для правила
/// </summary>
public class Rule : IRule
{
    /// <summary>
    /// Название правила
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Ip-адрес источника запроса
    /// </summary>
    public string SourceIp { get; set; } = string.Empty;

    /// <summary>
    /// Метод запроса
    /// </summary>
    public string HttpMethod { get; set; } = string.Empty;

    /// <summary>
    /// Эндпоинт, для которого настраивается правило
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// Политика доступа
    /// </summary>
    public AccessPolicy AccessPolicy { get; set; }

    /// <summary>
    /// Правила для отдельных параметров запроса
    /// </summary>
    public List<IParameterRule> ArgumentRules { get; set; } = [];

    /// <summary>
    /// Анализаторы последовательности запросов
    /// </summary>
    public List<ISequenceAnalysis> SequenceAnalyses { get; set; } = [];

    /// <summary>
    /// Условие комбинации правил
    /// </summary>
    public string ParameterRulesCombination { get; set; } = string.Empty;

    public virtual Task<bool> IsRequestValid(HttpContext context)
    {
        var isRequestEthalon = false;

        var clientIp = context.Connection.RemoteIpAddress.ToString();
        var endpoint = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;

        if (DidEndpointMatch(endpoint))
            isRequestEthalon = true;
            
        if (!string.IsNullOrEmpty(SourceIp))
            isRequestEthalon = isRequestEthalon && clientIp.Equals(SourceIp, StringComparison.InvariantCultureIgnoreCase);

        if (!string.IsNullOrEmpty(HttpMethod))
            isRequestEthalon = isRequestEthalon && context.Request.Method.Equals(HttpMethod, StringComparison.InvariantCultureIgnoreCase);

        if (ParameterRules.Count > 0)
            isRequestEthalon = isRequestEthalon && AreParamtersEqualToDeclaredEthalons(context.Request);

        return AccessPolicy switch
        {
            AccessPolicy.Allow => Task.FromResult(isRequestEthalon),
            AccessPolicy.Deny => Task.FromResult(!isRequestEthalon),
            _ => Task.FromResult(false)
        };
    }

    public virtual async Task<bool> IsRequestSequenceValid(HttpContext context)
    {
        var clientIp = context.Connection.RemoteIpAddress.ToString();

        foreach (var sequenceAnalysis in SequenceAnalyses)
        {
            var didAnalysisSucceed = await sequenceAnalysis.DidAnalysisSucceed(clientIp);

            if (!didAnalysisSucceed)
                return false;
        }

        return true;
    }

    private bool DidEndpointMatch(string endpoint)
    {
        return endpoint.Equals(Endpoint, StringComparison.InvariantCultureIgnoreCase);
    }

    private bool AreParamtersEqualToDeclaredEthalons(HttpRequest request)
    {
        var totalBrokenRulesCount = 0;

        foreach (var parameterRule in ArgumentRules)
        {
            var currentArguments = HttpUtility.ParseQueryString(request.QueryString.Value).ToDictionary();

            var parameterValidationResult = parameterRule.Validate(currentArguments);

            if (parameterValidationResult)
                totalBrokenRulesCount++;
        }

        if (totalBrokenRulesCount == ArgumentRules.Count)
            return true;

        return false;
    }
}