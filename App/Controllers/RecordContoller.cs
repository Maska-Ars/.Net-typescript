using App.Contracts;
using App.DataAccess;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.Controllers;

/// <summary>
/// Контроллер для управления записями (Records).
/// </summary>
[ApiController]
[Route("[controller]")]
public class RecordController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Возвращает список всех записей, включая информацию о связанных релизах.
    /// </summary>
    /// <returns>
    /// IActionResult: Список объектов Record или ошибку, если произошла ошибка при получении данных из базы данных.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var records = await _context.Records.Include(r => r.Release).ToListAsync();
        return Ok(records);
    }

    /// <summary>
    /// Возвращает запись по указанному ID, включая информацию о связанном релизе.
    /// </summary>
    /// <param name="id">Идентификатор записи (GUID).</param>
    /// <returns>
    /// IActionResult: Объект Record или NotFound(), если запись не найдена.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var record = await _context.Records.Include(r => r.Release).FirstOrDefaultAsync(r => r.Id == id);

        if (record == null)
        {
            return NotFound();
        }

        return Ok(record);
    }

    /// <summary>
    /// Создает новую запись.
    /// </summary>
    /// <param name="request">Объект CreateRecordRequest, содержащий данные новой записи.</param>
    /// <returns>
    /// IActionResult:  CreatedAtAction с данными новой записи, если создание прошло успешно.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRecordRequest request)
    {
        var record = new Record
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            DurationSeconds = request.DurationSeconds,
            ReleaseId = request.ReleaseId
        };

        _context.Records.Add(record);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
    }

    // <summary>
    /// Обновляет информацию о записи.  Поля, которые не указаны в UpdateRecordRequest, останутся неизменными.
    /// </summary>
    /// <param name="id">Идентификатор записи (GUID).</param>
    /// <param name="request">Объект UpdateRecordRequest, содержащий данные для обновления.</param>
    /// <returns>
    /// IActionResult: Обновленный объект Record или NotFound(), если запись не найдена.
    /// </returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRecordRequest request)
    {
        var record = await _context.Records.FindAsync(id);

        if (record == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            record.Name = request.Name;
        }

        if (request.DurationSeconds.HasValue)
        {
            record.DurationSeconds = request.DurationSeconds.Value;
        }

        if (request.ReleaseId.HasValue)
        {
            record.ReleaseId = request.ReleaseId.Value;
        }

        _context.Records.Update(record);

        await _context.SaveChangesAsync();

        return Ok(record);
    }

    /// <summary>
    /// Удаляет запись по указанному ID.
    /// </summary>
    /// <param name="id">Идентификатор записи (GUID).</param>
    /// <returns>
    /// IActionResult: NoContent(), если удаление прошло успешно, или NotFound(), если запись не найдена.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var record = await _context.Records.FindAsync(id);

        if (record == null)
        {
            return NotFound();
        }

        _context.Records.Remove(record);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
