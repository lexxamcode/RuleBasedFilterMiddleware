namespace RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;

public class RawParameterRule
{
    /// <summary>
    /// Имя параметра
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Тип параметра
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Эталонное значение параметра
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Логическое условие для данного параметра
    /// </summary>
    public string ShouldBe { get; set; } = string.Empty;
}