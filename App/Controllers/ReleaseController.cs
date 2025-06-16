using App.Contracts;
using App.DataAccess;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.Controllers;

/// <summary>
/// ���������� ��� ���������� �������� (Releases).
/// </summary>
[ApiController]
[Route("[controller]")]
public class ReleaseController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // <summary>
    /// ���������� ������ ���� �������.
    /// </summary>
    /// <returns>
    /// IActionResult: ������ �������� Release. 
    /// ���������� ������, ���� ��������� ������ ��� ��������� ������ �� ���� ������.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var releases = await _context.Releases.ToListAsync();
        return Ok(releases);
    }

    /// <summary>
    /// ���������� ����� �� ���������� ID.
    /// </summary>
    /// <param name="id">������������� ������ (GUID).</param>
    /// <returns>
    /// IActionResult: ������ Release ��� NotFound(), ���� ����� �� ������.
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
    /// ������� ����� �����.
    /// </summary>
    /// <param name="request">������ CreateReleaseRequest, ���������� ������ ������ ������.</param>
    /// <returns>
    /// IActionResult: CreatedAtAction � ������� ������ ������, ���� �������� ������ �������.
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
    /// ��������� ���������� � ������. ����, �� ��������� � UpdateReleaseRequest, ��������� �����������.
    /// </summary>
    /// <param name="id">������������� ������ (GUID).</param>
    /// <param name="request">������ UpdateReleaseRequest, ���������� ������ ��� ����������.</param>
    /// <returns>
    /// IActionResult: ����������� ������ Release ��� NotFound(), ���� ����� �� ������.
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
    /// ������� ����� �� ���������� ID.
    /// </summary>
    /// <param name="id">������������� ������ (GUID).</param>
    /// <returns>
    /// IActionResult: NoContent(), ���� �������� ������ �������, ��� NotFound(), ���� ����� �� ������.
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
