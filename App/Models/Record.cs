using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

// <summary>
/// ������������ ������.
/// </summary>
public class Record
{
    /// <summary>
    /// ���������� ������������� ������ (GUID).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// �������� ������. ������������ ����.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// ����������������� ������ � ��������.
    /// </summary>
    public int DurationSeconds { get; set; }

    // <summary>
    /// ������������� ������, � �������� ����������� ������. ������������ ����.
    /// </summary>
    [Required]
    public Guid ReleaseId { get; set; }

    /// <summary>
    /// ��������� �����.  ������������ ��� ��������� �� ����� "����-��-������" � �������� Release.
    /// </summary>
    [ForeignKey("ReleaseId")]
    public Release? Release { get; set; }
}
