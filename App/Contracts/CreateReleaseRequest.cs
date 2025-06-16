namespace App.Contracts;

/// <summary>
/// Запрос на создание нового релиза.
/// </summary>
public record CreateReleaseRequest(string Name, string? Image, Guid ArtistId);

/// <summary>
/// Запрос на обновление информации о релизе.
/// </summary>
public record UpdateReleaseRequest(string? Name, string? Image, Guid? ArtistId);