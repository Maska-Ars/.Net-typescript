namespace App.Contracts;

/// <summary>
/// Запрос на создание новой записи.
/// </summary>
public record CreateRecordRequest(string Name, int DurationSeconds, Guid ReleaseId);

/// <summary>
/// Запрос на обновление информации о записи.
/// </summary>
public record UpdateRecordRequest(string? Name, int? DurationSeconds, Guid? ReleaseId);
