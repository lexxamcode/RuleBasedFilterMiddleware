using Microsoft.AspNetCore.Mvc;

namespace TileServerApi.Model;

/// <summary>
/// Интерфейс репозитория тайлов
/// </summary>
public interface ITileRepository
{
    /// <summary>
    /// Получение тайла по его координатам
    /// </summary>
    /// <param name="z">Координата z (приближение)</param>
    /// <param name="x">Координата x</param>
    /// <param name="y">Координата y</param>
    /// <returns>Изображение тайла</returns>
    public FileResult GetTile(string z, string x, string y);
}