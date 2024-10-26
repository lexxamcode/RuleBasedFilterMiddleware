namespace YmlRulesFileParser.Model.Requests;

/// <summary>
/// Класс запроса
/// </summary>
public class Request
{
    /// <summary>
    /// IP-адрес клиента, отправившего запрос
    /// </summary>
    public string ClientIp { get; set; } = string.Empty;

    /// <summary>
    /// Метод запроса
    /// </summary>
    public HttpMethod? Method { get; set; }

    /// <summary>
    /// Параметры запроса
    /// </summary>
    public Parameters Parameters { get; set; } = new();

    /// <summary>
    /// Время запроса
    /// </summary>
    public DateTime RequestTime { get; set; }
}