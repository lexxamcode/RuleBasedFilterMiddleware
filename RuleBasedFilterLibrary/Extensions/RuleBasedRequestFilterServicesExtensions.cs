using Microsoft.Extensions.DependencyInjection;
using OpenSearch.Client;
using RuleBasedFilterLibrary.Core.Services.RequestSequenceAnalysis;
using RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;
using RuleBasedFilterLibrary.Core.Services.RequestStorageManager;
using RuleBasedFilterLibrary.Infrastructure.Services.RequestsStorage;
using RuleBasedFilterLibrary.Infrastructure.Services.RulesFileParsing;
using RuleBasedFilterLibrary.Middleware.Services.RequestValidation;

namespace RuleBasedFilterLibrary.Extensions;

public static class RuleBasedRequestFilterServicesExtensions
{
    public static IServiceCollection AddRuleBasedRequestFilterServices(this IServiceCollection services, RuleBasedRequestFilterOptions options)
    {
        services.AddSingleton(options);
        services.AddSingleton<IRulesLoader, RulesLoader>();
        services.AddSingleton<IRequestValidationService, RequestValidationService>();

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
        services.AddSingleton<IRequestStorage, OpensearchRequestStorage>();
        services.AddSingleton<IRequestStorageManager, RequestStorageManager>();
        services.AddSingleton<IRequestSequenceAnalyzer, MonotonicityAnalyzer>();

        return services;
    }
}
