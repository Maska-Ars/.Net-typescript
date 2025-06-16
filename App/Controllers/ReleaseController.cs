using App.Contracts;
using App.DataAccess;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.Controllers;

/// <summary>
/// Контроллер для управления релизами (Releases).
/// </summary>
[ApiController]
[Route("[controller]")]
public class ReleaseController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // <summary>
    /// Возвращает список всех релизов.
    /// </summary>
    /// <returns>
    /// IActionResult: Список объектов Release. 
    /// Возвращает ошибку, если произошла ошибка при получении данных из базы данных.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var releases = await _context.Releases.ToListAsync();
        return Ok(releases);
    }

    /// <summary>
    /// Возвращает релиз по указанному ID.
    /// </summary>
    /// <param name="id">Идентификатор релиза (GUID).</param>
    /// <returns>
    /// IActionResult: Объект Release или NotFound(), если релиз не найден.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var release = await _context.Releases.FindAsync(id);

        if (release == null)
        {
            return NotFound();
        }

        return Ok(release);
    }

    /// <summary>
    /// Создает новый релиз.
    /// </summary>
    /// <param name="request">Объект CreateReleaseRequest, содержащий данные нового релиза.</param>
    /// <returns>
    /// IActionResult: CreatedAtAction с данными нового релиза, если создание прошло успешно.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReleaseRequest request)
    {
        var release = new Release
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Image = request.Image,
            ArtistId = request.ArtistId
        };

        _context.Releases.Add(release);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = release.Id }, release);
    }

    /// <summary>
    /// Обновляет информацию о релизе. Поля, не указанные в UpdateReleaseRequest, останутся неизменными.
    /// </summary>
    /// <param name="id">Идентификатор релиза (GUID).</param>
    /// <param name="request">Объект UpdateReleaseRequest, содержащий данные для обновления.</param>
    /// <returns>
    /// IActionResult: Обновленный объект Release или NotFound(), если релиз не найден.
    /// </returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReleaseRequest request)
    {
        var release = await _context.Releases.FindAsync(id);

        if (release == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            release.Name = request.Name;
        }

        if (request.Image != null)
        {
            release.Image = request.Image;
        }

        if (request.ArtistId.HasValue)
        {
            release.ArtistId = request.ArtistId.Value;
        }

        _context.Releases.Update(release);

        await _context.SaveChangesAsync();

        return Ok(release);
    }

    /// <summary>
    /// Удаляет релиз по указанному ID.
    /// </summary>
    /// <param name="id">Идентификатор релиза (GUID).</param>
    /// <returns>
    /// IActionResult: NoContent(), если удаление прошло успешно, или NotFound(), если релиз не найден.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var release = await _context.Releases.FindAsync(id);

        if (release == null)
        {
            return NotFound();
        }

        _context.Releases.Remove(release);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
