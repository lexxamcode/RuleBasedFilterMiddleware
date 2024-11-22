using Microsoft.AspNetCore.Http;

namespace YmlRulesFileParser.Model.Rules.Base;

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
    public Dictionary<string, RequestParameterRule> ParameterRules { get; set; } = [];

    public virtual Task<bool> IsRequestValid(HttpRequest request)
    {
        var isRequestEthalon = false;

        var clientIp = GetSourceIpFromRequest(request);
        var enpoint = request.Path.HasValue ? request.Path.Value : string.Empty;

        if (clientIp == SourceIp &&
            HttpMethod.Equals(request.Method, StringComparison.InvariantCultureIgnoreCase) &&
            Endpoint.Equals(enpoint, StringComparison.InvariantCulture))
            isRequestEthalon = true;

        return AccessPolicy switch
        {
            AccessPolicy.Allow => Task.FromResult(isRequestEthalon),
            AccessPolicy.Deny => Task.FromResult(!isRequestEthalon),
            _ => Task.FromResult(false)
        };
    }

    private static string GetSourceIpFromRequest(HttpRequest request)
    {
        return request.Headers["X-Forwarded-For"].ToString() ??
            throw new Exception("Could not get X-Forwarded-For header for request");
    }
}