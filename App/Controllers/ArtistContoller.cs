using App.Contracts;
using App.DataAccess;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.Controllers;

/// <summary>
/// ���������� ��� ������ � ������������� (Artists).
/// </summary>
[ApiController]
[Route("[controller]")]
public class ArtistController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// ��������� ���������� �� ����������� �� ��� ID.
    /// </summary>
    /// <param name="id">������������� ����������� (GUID).</param>
    /// <returns>
    /// IActionResult:  ���������� ������ Artist, ���� ������, NotFound() ���� ���.
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
    /// ��������� ������ ���� ������������.
    /// </summary>
    /// <returns>
    /// IActionResult: ���������� ������ ���� �������� Artist.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var artists = await _context.Artists.ToListAsync();
        return Ok(artists);
    }

    /// <summary>
    /// �������� ������ �����������.
    /// </summary>
    /// <param name="request">
    /// ������ CreateArtistRequest, ���������� ������ ������ �����������.
    /// </param>
    /// <returns>
    /// IActionResult: ���������� CreatedAtAction � ������� ������ �����������, ���� �������� ������ �������.
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
    /// ���������� ���������� �� �����������.
    /// </summary>
    /// <param name="id">������������� ����������� (GUID).</param>
    /// <param name="request">������ UpdateArtistRequest, ���������� ������ ��� ����������.</param>
    /// <returns>
    /// IActionResult: ���������� ����������� ������ Artist, ���� ���������� ������ �������,
    /// NotFound() ���� ����������� �� ������.
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
    /// �������� �����������.
    /// </summary>
    /// <param name="id">������������� ����������� (GUID).</param>
    /// <returns>
    /// IActionResult: ���������� NoContent(), ���� �������� ������ �������, NotFound() ���� ����������� �� ������.
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
