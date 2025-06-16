using App.Contracts;
using App.DataAccess;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.Controllers;

/// <summary>
/// Контроллер для работы с исполнителями (Artists).
/// </summary>
[ApiController]
[Route("[controller]")]
public class ArtistController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Получение информации об исполнителе по его ID.
    /// </summary>
    /// <param name="id">Идентификатор исполнителя (GUID).</param>
    /// <returns>
    /// IActionResult:  Возвращает объект Artist, если найден, NotFound() если нет.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var record = await _context.Artists.Include(r => r.Id).FirstOrDefaultAsync(r => r.Id == id);

        if (record == null)
        {
            return NotFound();
        }

        return Ok(record);
    }

    /// <summary>
    /// Получение списка всех исполнителей.
    /// </summary>
    /// <returns>
    /// IActionResult: Возвращает список всех объектов Artist.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var artists = await _context.Artists.ToListAsync();
        return Ok(artists);
    }

    /// <summary>
    /// Создание нового исполнителя.
    /// </summary>
    /// <param name="request">
    /// Объект CreateArtistRequest, содержащий данные нового исполнителя.
    /// </param>
    /// <returns>
    /// IActionResult: Возвращает CreatedAtAction с данными нового исполнителя, если создание прошло успешно.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArtistRequest request)
    {
        var artist = new Artist(request.Name, request.Country) 
        { 
            Id = Guid.NewGuid() 
        };

        _context.Artists.Add(artist);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = artist.Id }, artist);
    }

    /// <summary>
    /// Обновление информации об исполнителе.
    /// </summary>
    /// <param name="id">Идентификатор исполнителя (GUID).</param>
    /// <param name="request">Объект UpdateArtistRequest, содержащий данные для обновления.</param>
    /// <returns>
    /// IActionResult: Возвращает обновленный объект Artist, если обновление прошло успешно,
    /// NotFound() если исполнитель не найден.
    /// </returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateArtistRequest request)
    {
        var artist = await _context.Artists.FindAsync(id); 

        if (artist == null)
        {
            return NotFound();
        }
         
        if (!string.IsNullOrEmpty(request.Name))
        {
            artist.Name = request.Name;
        }

        if (!string.IsNullOrEmpty(request.Country))
        {
            artist.Country = request.Country;
        }

        _context.Artists.Update(artist);

        await _context.SaveChangesAsync(); 

        return Ok(artist);
    }

    /// <summary>
    /// Удаление исполнителя.
    /// </summary>
    /// <param name="id">Идентификатор исполнителя (GUID).</param>
    /// <returns>
    /// IActionResult: Возвращает NoContent(), если удаление прошло успешно, NotFound() если исполнитель не найден.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var artist = await _context.Artists.FindAsync(id);

        if (artist == null)
        {
            return NotFound();
        }

        _context.Artists.Remove(artist);

        await _context.SaveChangesAsync(); 

        return NoContent();
    }
}
