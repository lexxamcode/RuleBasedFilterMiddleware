﻿namespace YmlRulesFileParser.Model.Rules.Raw;

public class RawRequestRule
{
    /// <summary>
    /// Название правила
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// На какой метод распространяется правило
    /// </summary>
    public string RequestMethod { get; set; } = string.Empty;

    /// <summary>
    /// Api-метод, на который распространяется правило
    /// </summary>
    public string ForEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Ip-адрес источника запроса
    /// </summary>
    public string SourceIp { get; set; } = string.Empty;

    /// <summary>
    /// Действие (Разрешить, запретить)
    /// </summary>
    public string AccessPolicy { get; set; } = string.Empty;

    /// <summary>
    /// Логическое выражение для комбинации всех параметров в один фильтр
    /// </summary>
    public string ParameterRulesCombination { get; set; } = string.Empty;

    /// <summary>
    /// Правила для отдельных параметров запроса
    /// </summary>
    public Dictionary<string, RawRequestParameterRule> ParameterRules { get; set; } = [];
}