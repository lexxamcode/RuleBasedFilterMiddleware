using Microsoft.Extensions.DependencyInjection;
using RuleBasedFilterLibrary.Services;
using RuleBasedFilterLibrary.Services.DeepAnalysis;

namespace RuleBasedFilterLibrary.Extensions;

public static class RuleBasedRequestFilterServicesExtensions
{
    public static IServiceCollection AddRuleBasedRequestFilterServices(this IServiceCollection services)
    {
        services.AddSingleton<IRulesLoaderService, RulesLoaderService>();
        services.AddSingleton<IRequestSequenceAnalyzer, RequestSequenceAnalyzer>();
        return services;
    }
}
