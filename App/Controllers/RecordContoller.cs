using App.Contracts;
using App.DataAccess;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.Controllers;

/// <summary>
/// ���������� ��� ���������� �������� (Records).
/// </summary>
[ApiController]
[Route("[controller]")]
public class RecordController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// ���������� ������ ���� �������, ������� ���������� � ��������� �������.
    /// </summary>
    /// <returns>
    /// IActionResult: ������ �������� Record ��� ������, ���� ��������� ������ ��� ��������� ������ �� ���� ������.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var records = await _context.Records.Include(r => r.Release).ToListAsync();
        return Ok(records);
    }

    /// <summary>
    /// ���������� ������ �� ���������� ID, ������� ���������� � ��������� ������.
    /// </summary>
    /// <param name="id">������������� ������ (GUID).</param>
    /// <returns>
    /// IActionResult: ������ Record ��� NotFound(), ���� ������ �� �������.
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
    /// ������� ����� ������.
    /// </summary>
    /// <param name="request">������ CreateRecordRequest, ���������� ������ ����� ������.</param>
    /// <returns>
    /// IActionResult:  CreatedAtAction � ������� ����� ������, ���� �������� ������ �������.
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
    /// ��������� ���������� � ������.  ����, ������� �� ������� � UpdateRecordRequest, ��������� �����������.
    /// </summary>
    /// <param name="id">������������� ������ (GUID).</param>
    /// <param name="request">������ UpdateRecordRequest, ���������� ������ ��� ����������.</param>
    /// <returns>
    /// IActionResult: ����������� ������ Record ��� NotFound(), ���� ������ �� �������.
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
    /// ������� ������ �� ���������� ID.
    /// </summary>
    /// <param name="id">������������� ������ (GUID).</param>
    /// <returns>
    /// IActionResult: NoContent(), ���� �������� ������ �������, ��� NotFound(), ���� ������ �� �������.
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
