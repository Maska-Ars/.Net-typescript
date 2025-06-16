using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

/// <summary>
/// Представляет релиз.
/// </summary>
public class Release
{
    /// <summary>
    /// Уникальный идентификатор релиза (GUID).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название релиза. Обязательное поле.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// URL или путь к изображению релиза.  Может быть null.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    ///  Идентификатор релиза.
    /// </summary>
    [Required]
    public Guid ReleaseId { get; set; }

    /// <summary>
    /// Идентификатор исполнителя, выпустившего релиз. Обязательное поле.
    /// </summary>
    [Required]
    public Guid ArtistId { get; set; }

    // <summary>
    /// Связанный исполнитель. Используется для навигации по связи "один-ко-многим" с таблицей Artist.
    /// </summary>
    [ForeignKey("ArtistId")]
    public Artist? Artist { get; set; }

    /// <summary>
    /// Дата добавления релиза (UTC).  Устанавливается по умолчанию при создании объекта.
    /// </summary>
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
}