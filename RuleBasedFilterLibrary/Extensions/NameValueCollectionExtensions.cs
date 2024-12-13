using System.Collections.Specialized;

namespace RuleBasedFilterLibrary.Extensions;

public static class NameValueCollectionExtensions
{
    public static Dictionary<string, string> ToDictionary(this NameValueCollection nvc)
    {
        if (nvc is null)
            return [];

        return nvc.AllKeys.ToDictionary(key => key ?? string.Empty, k => nvc[k] ?? string.Empty);
    }
}
