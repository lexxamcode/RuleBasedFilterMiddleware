namespace RuleBasedFilterLibrary.Model.Requests;

public class Request
{
    public string UserIp { get; set; } = string.Empty;
    public DateTime RequestTime { get; set; }
    public Dictionary<string, string> Parameters { get; set; } = [];
}
