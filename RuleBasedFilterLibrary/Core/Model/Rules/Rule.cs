using Microsoft.AspNetCore.Http;
using RuleBasedFilterLibrary.Core.Model.AccessPolicies;
using RuleBasedFilterLibrary.Core.Model.ParameterRules;
using RuleBasedFilterLibrary.Core.Model.SequenceAnalyses;
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
    public List<ParameterRule> ParameterRules { get; set; } = [];

    /// <summary>
    /// Анализаторы последовательности запросов
    /// </summary>
    public List<SequenceAnalysis> SequenceAnalyses { get; set; } = [];

    /// <summary>
    /// Условие комбинации правил
    /// </summary>
    public string ParameterRulesCombination { get; set; } = string.Empty;

    public virtual Task<bool> IsRequestValid(HttpContext context)
    {
        var isRequestEthalon = false;

        var clientIp = context.Connection.RemoteIpAddress.ToString();
        var endpoint = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;

        if (CheckIpAddress(clientIp) &&
            CheckHttpMethod(context) &&
            CheckEndpoint(endpoint) &&
            AreParamtersEqualToDeclaredEthalons(context.Request))
            isRequestEthalon = true;

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
            var didAnalysisSucceed = await sequenceAnalysis.Analyse(clientIp);

            if (!didAnalysisSucceed)
                return false;
        }

        return true;
    }

    private bool CheckIpAddress(string ipAddress)
    {
        if (string.IsNullOrEmpty(SourceIp))
            return true;

        return ipAddress.Equals(SourceIp, StringComparison.InvariantCultureIgnoreCase);
    }

    private bool CheckHttpMethod(HttpContext context)
    {
        if (string.IsNullOrEmpty(HttpMethod))
            return true;

        return context.Request.Method.Equals(HttpMethod, StringComparison.InvariantCultureIgnoreCase);
    }

    private bool CheckEndpoint(string endpoint)
    {
        if (string.IsNullOrEmpty(Endpoint))
            return true;

        return endpoint.Equals(Endpoint, StringComparison.InvariantCultureIgnoreCase);
    }

    private bool AreParamtersEqualToDeclaredEthalons(HttpRequest request)
    {
        if (ParameterRules.Count == 0) return false;

        var totalBrokenRulesCount = 0;

        foreach (var parameterRule in ParameterRules)
        {
            var parameterValueAsString = HttpUtility
                .ParseQueryString(request.QueryString.Value)
                .Get(parameterRule.Name) ??
                throw new ArgumentException("Failed to parse request parameter");

            var parameterValidationResult = parameterRule.CompareTo(parameterValueAsString);

            if (parameterValidationResult)
                totalBrokenRulesCount++;
        }

        if (totalBrokenRulesCount == ParameterRules.Count)
            return true;

        return false;
    }
}