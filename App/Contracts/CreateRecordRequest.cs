namespace App.Contracts;

/// <summary>
/// ������ �� �������� ����� ������.
/// </summary>
public record CreateRecordRequest(string Name, int DurationSeconds, Guid ReleaseId);

/// <summary>
/// ������ �� ���������� ���������� � ������.
/// </summary>
public record UpdateRecordRequest(string? Name, int? DurationSeconds, Guid? ReleaseId);
