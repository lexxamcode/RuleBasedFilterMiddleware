namespace YmlRulesFileParser.Model.Rules.Base;


/// <summary>
/// Базовый абстрактный класс для правила
/// </summary>
public class Rule
{
    /// <summary>
    /// Название правила
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// На какой метод распространяется правило
    /// </summary>
    public string Method { get; set; } = string.Empty;

    /// <summary>
    /// Ip-адрес источника запроса
    /// </summary>
    public string SourceIp { get; set; } = string.Empty;

    /// <summary>
    /// Действие (Разрешить, запретить)
    /// </summary>
    public string AccessPolicy { get; set; } = string.Empty;

    /// <summary>
    /// Правила для отдельных параметров запроса
    /// </summary>
    public Dictionary<string, ParameterRuleEntry> Parameters { get; set; } = [];
}