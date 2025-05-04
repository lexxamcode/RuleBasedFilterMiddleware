using OpenSearch.Client;
using RuleBasedFilterLibrary.Core.Model.Requests;

namespace TestTileApi.Utils;

public static class OnViolationHandler
{
    public static async Task OnViolationAction(Request request)
    {
        var nodeAddress = new Uri("http://localhost:9200");
        var config = new ConnectionSettings(nodeAddress).DefaultIndex("requests");

        var openSearchClient = new OpenSearchClient(config);

        var deleteRequest = new DeleteIndexRequest(Indices.Parse("requests"));
        await openSearchClient.Indices.DeleteAsync(deleteRequest);
    }
}
