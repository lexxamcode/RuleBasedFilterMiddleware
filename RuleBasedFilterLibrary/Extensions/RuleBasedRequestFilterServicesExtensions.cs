using Microsoft.Extensions.DependencyInjection;
using OpenSearch.Client;
using RuleBasedFilterLibrary.Services;
using RuleBasedFilterLibrary.Services.DeepAnalysis;

namespace RuleBasedFilterLibrary.Extensions;

public static class RuleBasedRequestFilterServicesExtensions
{
    public static IServiceCollection AddRuleBasedRequestFilterServices(this IServiceCollection services)
    {
        services.AddSingleton<IRulesLoaderService, RulesLoaderService>();
        services.AddSingleton<IRequestSequenceAnalyzer, RequestSequenceAnalyzer>();

        var nodeAddress = new Uri("http://localhost:9200");
        var config = new ConnectionSettings(nodeAddress).DefaultIndex("requests");
        var client = new OpenSearchClient(config);
        var deleteRequest = new DeleteIndexRequest(Indices.Parse("requests"));
        client.Indices.Delete(deleteRequest);

        services.AddSingleton<IOpenSearchClient>(client);

        return services;
    }
}
