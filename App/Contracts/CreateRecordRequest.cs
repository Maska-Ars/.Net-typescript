namespace App.Contracts;

public record CreateRecordRequest(string Name, int DurationSeconds, Guid ReleaseId);
public record UpdateRecordRequest(string? Name, int? DurationSeconds, Guid? ReleaseId);
