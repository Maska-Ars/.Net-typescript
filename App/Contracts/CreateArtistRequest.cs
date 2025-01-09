namespace App.Contracts;

public record CreateArtistRequest(string Name, string Country);
public record UpdateArtistRequest(string Name, string Country);