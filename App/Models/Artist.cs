using Microsoft.AspNetCore.SignalR;

namespace App.Models;

/// <summary>
/// ������������ �����������.
/// </summary>
public class Artist(string name, string country)
{
    /// <summary>
    /// ���������� ������������� ����������� (GUID).
    /// </summary>
    public Guid Id {get; set;}

    /// <summary>
    /// ��� �����������.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// ������ �����������.
    /// </summary>
    public string Country { get; set; } = country;
}