using Microsoft.AspNetCore.Mvc;
using TileServerApi.Model;

namespace TileServerApi.Services;

/// <summary>
/// Сервис для выдачи изображений тайлов из 
/// директории локальной файловой системы
/// </summary>
public class LocalTileRepository(string directory) : ITileRepository
{
    private readonly string _directory = directory;

    public FileResult GetTile(string z, string x, string y)
    {
        var tilePath = Path.Combine(_directory, z, x, y) + ".png";
        return new PhysicalFileResult(tilePath, "image/png");
    }
}
