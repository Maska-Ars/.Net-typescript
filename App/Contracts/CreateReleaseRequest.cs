namespace App.Contracts;

/// <summary>
/// ������ �� �������� ������ ������.
/// </summary>
public record CreateReleaseRequest(string Name, string? Image, Guid ArtistId);

/// <summary>
/// ������ �� ���������� ���������� � ������.
/// </summary>
public record UpdateReleaseRequest(string? Name, string? Image, Guid? ArtistId);