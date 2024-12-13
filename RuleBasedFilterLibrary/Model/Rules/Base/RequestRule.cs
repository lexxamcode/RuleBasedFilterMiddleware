using Microsoft.AspNetCore.Http;
using System.Web;

namespace RuleBasedFilterLibrary.Model.Rules.Base;

/// <summary>
/// Базовый класс для правила
/// </summary>
public class RequestRule : IRule
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
    public List<RequestParameterRule> ParameterRules { get; set; } = [];

    /// <summary>
    /// Условие комбинации правил
    /// </summary>
    public string ParameterRulesCombination { get; set; } = string.Empty;

    public virtual Task<bool> IsRequestValid(HttpContext context)
    {
        var isRequestEthalon = false;

        var clientIp = context.Connection.RemoteIpAddress.ToString();
        var enpoint = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;

        if (clientIp == SourceIp &&
            HttpMethod.Equals(context.Request.Method, StringComparison.InvariantCultureIgnoreCase) &&
            Endpoint.Equals(enpoint, StringComparison.InvariantCulture) &&
            SourceIp.Equals(clientIp) &&
            AreParamtersEqualToDeclaredEthalons(context.Request))
            isRequestEthalon = true;

        return AccessPolicy switch
        {
            AccessPolicy.Allow => Task.FromResult(isRequestEthalon),
            AccessPolicy.Deny => Task.FromResult(!isRequestEthalon),
            _ => Task.FromResult(false)
        };
    }

    private bool AreParamtersEqualToDeclaredEthalons(HttpRequest request)
    {
        foreach (var parameterRule in ParameterRules)
        {
            var parameterValueAsString = HttpUtility
                .ParseQueryString(request.QueryString.Value)
                .Get(parameterRule.Name) ??
                throw new ArgumentException("Failed to parse request parameter");

            var parameterValidationResult = parameterRule.CompareTo(parameterValueAsString);

            if (!parameterValidationResult)
                return false;
        }

        return true;
    }
}