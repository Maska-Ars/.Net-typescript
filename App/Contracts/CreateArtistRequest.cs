namespace App.Contracts;

/// <summary>
/// ������ �� �������� ������ �����������.
/// </summary>
public record CreateArtistRequest(string Name, string Country);

/// <summary>
/// ������ �� ���������� ���������� �� �����������.
/// </summary>
public record UpdateArtistRequest(string Name, string Country);