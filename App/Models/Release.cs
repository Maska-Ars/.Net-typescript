using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

/// <summary>
/// ������������ �����.
/// </summary>
public class Release
{
    /// <summary>
    /// ���������� ������������� ������ (GUID).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// �������� ������. ������������ ����.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// URL ��� ���� � ����������� ������.  ����� ���� null.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    ///  ������������� ������.
    /// </summary>
    [Required]
    public Guid ReleaseId { get; set; }

    /// <summary>
    /// ������������� �����������, ������������ �����. ������������ ����.
    /// </summary>
    [Required]
    public Guid ArtistId { get; set; }

    // <summary>
    /// ��������� �����������. ������������ ��� ��������� �� ����� "����-��-������" � �������� Artist.
    /// </summary>
    [ForeignKey("ArtistId")]
    public Artist? Artist { get; set; }

    /// <summary>
    /// ���� ���������� ������ (UTC).  ��������������� �� ��������� ��� �������� �������.
    /// </summary>
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
}