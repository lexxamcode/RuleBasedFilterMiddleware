using RuleBasedFilterLibrary.Infrastructure.Model.RawParameterRules;
using RuleBasedFilterLibrary.Infrastructure.Model.RawSequenceAnalyses;

namespace RuleBasedFilterLibrary.Infrastructure.Model.RawRules;

public class RawRule
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
    /// Api-метод, на который распространяется правило
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

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
    public List<RawParameterRule> ParameterRules { get; set; } = [];

    public List<RawSequenceAnalysis> SequenceAnalyses { get; set; } = []; 
}