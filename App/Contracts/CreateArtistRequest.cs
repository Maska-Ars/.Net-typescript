namespace App.Contracts;

/// <summary>
/// Запрос на создание нового исполнителя.
/// </summary>
public record CreateArtistRequest(string Name, string Country);

/// <summary>
/// Запрос на обновление информации об исполнителе.
/// </summary>
public record UpdateArtistRequest(string Name, string Country);