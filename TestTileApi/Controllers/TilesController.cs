using Microsoft.AspNetCore.Mvc;
using OpenSearch.Client;

namespace TestTileApi.Controllers;

/// <summary>
/// Контроллер для получения тайлов по их координатам
/// </summary>
/// <param name="tileRepository">Сервис работы с тайлами</param>
[ApiController]
[Route("[controller]")]
public class TilesController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TilesController> logger) : ControllerBase
{
    private readonly string _tileServerDomain = configuration["TileSourceConfiguration:TileSource"] ??
        throw new ArgumentNullException("Not found TileSourceConfiguration:TileSource section");

    private readonly string _tileServerApiKey = configuration["TileSourceConfiguration:TileSourceApiKey"] ??
        throw new ArgumentNullException("Not found TileSourceConfiguration:TileSourceApiKey section");

    /// <summary>
    /// Метод получения тайла по его координатам
    /// </summary>
    /// <param name="z">Координата z (приближение)</param>
    /// <param name="x">Координата x</param>
    /// <param name="y">Координата y</param>
    /// <returns>Изображение тайла</returns>
    [HttpGet]
    public async Task<ActionResult> GetTile(int z, int x, int y)
    {
        logger.LogInformation($"Tile {z} {x} {y}");
        // Do nothing while testing
        return await Task.FromResult(Ok());
    }

    [HttpGet("clear")]
    public async Task<ActionResult> ClearOpenSearchIndex()
    {
        var nodeAddress = new Uri("http://localhost:9200");
        var config = new ConnectionSettings(nodeAddress).DefaultIndex("requests");

        var openSearchClient = new OpenSearchClient(config);

        var deleteRequest = new DeleteIndexRequest(Indices.Parse("requests"));
        await openSearchClient.Indices.DeleteAsync(deleteRequest);

        logger.LogInformation("Clear");

        return Ok();
    }
}
