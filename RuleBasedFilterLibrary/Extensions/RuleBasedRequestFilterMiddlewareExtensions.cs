using Microsoft.AspNetCore.Builder;
using RuleBasedFilterLibrary.Middleware;

namespace RuleBasedFilterLibrary.Extensions;

public static class RuleBasedRequestFilterMiddlewareExtensions
{
    public static IApplicationBuilder UseRuleBasedFilter(this IApplicationBuilder builder, RuleBasedRequestFilterOptions options)
    {
        return builder.UseMiddleware<RuleBasedRequestFilterMiddleware>(options);
    }
}
