namespace RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;

public class RawParameterRule
{
    /// <summary>
    /// Имя правила для параметра
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Тип правила для параметра
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Имя параметра
    /// </summary>
    public string ArgumentName { get; set; } = string.Empty;

    /// <summary>
    /// Тип параметра
    /// </summary>
    public string ArgumentType { get; set; } = string.Empty;

    /// <summary>
    /// Логическое условие для данного параметра
    /// </summary>
    public string ArgumentShouldBe { get; set; } = string.Empty;
}