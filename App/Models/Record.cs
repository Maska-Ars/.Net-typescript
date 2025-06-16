using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

// <summary>
/// Представляет запись.
/// </summary>
public class Record
{
    /// <summary>
    /// Уникальный идентификатор записи (GUID).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название записи. Обязательное поле.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Продолжительность записи в секундах.
    /// </summary>
    public int DurationSeconds { get; set; }

    // <summary>
    /// Идентификатор релиза, к которому принадлежит запись. Обязательное поле.
    /// </summary>
    [Required]
    public Guid ReleaseId { get; set; }

    /// <summary>
    /// Связанный релиз.  Используется для навигации по связи "один-ко-многим" с таблицей Release.
    /// </summary>
    [ForeignKey("ReleaseId")]
    public Release? Release { get; set; }
}
