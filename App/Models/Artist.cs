using Microsoft.AspNetCore.SignalR;

namespace App.Models;

/// <summary>
/// Представляет исполнителя.
/// </summary>
public class Artist(string name, string country)
{
    /// <summary>
    /// Уникальный идентификатор исполнителя (GUID).
    /// </summary>
    public Guid Id {get; set;}

    /// <summary>
    /// Имя исполнителя.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Страна исполнителя.
    /// </summary>
    public string Country { get; set; } = country;
}