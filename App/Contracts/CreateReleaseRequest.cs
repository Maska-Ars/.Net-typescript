namespace App.Contracts;

public record CreateReleaseRequest(string Name, string? Image, Guid ArtistId);
public record UpdateReleaseRequest(string? Name, string? Image, Guid? ArtistId);