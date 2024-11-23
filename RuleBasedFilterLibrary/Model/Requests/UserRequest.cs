using System.Collections.Specialized;

namespace RuleBasedFilterLibrary.Model.Requests;

public class UserRequest
{
    public string IpAddress { get; set; } = string.Empty;
    public NameValueCollection Parameters { get; set; } = [];
}
