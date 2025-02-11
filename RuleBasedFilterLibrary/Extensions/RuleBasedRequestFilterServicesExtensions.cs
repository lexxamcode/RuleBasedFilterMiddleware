using Microsoft.Extensions.DependencyInjection;
using OpenSearch.Client;
using RuleBasedFilterLibrary.Services;
using RuleBasedFilterLibrary.Services.DeepAnalysis;
using RuleBasedFilterLibrary.Services.Requestvalidation;
using RuleBasedFilterLibrary.Services.RequestValidation;

namespace RuleBasedFilterLibrary.Extensions;

public static class RuleBasedRequestFilterServicesExtensions
{
    public static IServiceCollection AddRuleBasedRequestFilterServices(this IServiceCollection services, RuleBasedRequestFilterOptions options)
    {
        services.AddSingleton(options);
        services.AddSingleton<IRequestValidationService, RequestValidationService>();
        services.AddSingleton<IRulesLoaderService, RulesLoaderService>();
        services.AddSingleton<IRequestSequenceAnalyzer, RequestSequenceAnalyzer>();

        if (options.EnableRequestSequenceValidation)
            services.AddRequestSequenceStorageService(options);

        return services;
    }

    private static IServiceCollection AddRequestSequenceStorageService(this IServiceCollection services, RuleBasedRequestFilterOptions options)
    {
        var nodeAddress = new Uri(options.NodeAddress);
        var config = new ConnectionSettings(nodeAddress).DefaultIndex(options.IndexName);
        var client = new OpenSearchClient(config);
        var deleteRequest = new DeleteIndexRequest(Indices.Parse(options.IndexName));
        client.Indices.Delete(deleteRequest);

        services.AddSingleton<IOpenSearchClient>(client);

        return services;
    }
}
