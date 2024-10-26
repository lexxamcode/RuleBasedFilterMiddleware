namespace YmlRulesFileParser.Model.Rules.Base;


/// <summary>
/// Правило для отдельного параметра
/// </summary>
public class ParameterRuleEntry
{
    /// <summary>
    /// Тип параметра
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Эталонное значение параметра
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Условие совпадения
    /// </summary>
    public string When { get; set; } = string.Empty;

    /// <summary>
    /// Условия несовпадения
    /// </summary>
    public string WhenNot { get; set; } = string.Empty;
}