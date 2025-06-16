using App.Models;
using Microsoft.EntityFrameworkCore;
namespace App.DataAccess;

/// <summary>
/// �������� Entity Framework Core ��� ������� � ���� ������ ����������.
/// ���������� PostgreSQL � �������� ������� ���������� ������ ������.
/// </summary>
public class AppDbContext(IConfiguration configuration) : DbContext
{
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// DbSet ��� �������� Artist.
    /// </summary>
    public DbSet<Artist> Artists => Set<Artist>();

    /// <summary>
    /// DbSet ��� �������� Release.
    /// </summary>
    public DbSet<Release> Releases => Set<Release>();

    /// <summary>
    /// DbSet ��� �������� Record.
    /// </summary>
    public DbSet<Record> Records => Set<Record>();

    /// <summary>
    /// ����� ��� ��������� ����������� � ���� ������.
    /// ���������� ������ ����������� �� ������������ ����������.
    /// </summary>
    /// <param name="optionsBuilder">������ DbContextOptionsBuilder ��� ��������� ����������.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
    }
}