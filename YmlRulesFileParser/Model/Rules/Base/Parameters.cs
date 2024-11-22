namespace YmlRulesFileParser.Model.Rules.Base;


/// <summary>
/// Класс параметров, передаваемых в запросе
/// </summary>
public class Parameters
{
    /// <summary>
    /// Параметры запроса
    /// </summary>
    public Dictionary<string, object> Data { get; set; } = [];
}