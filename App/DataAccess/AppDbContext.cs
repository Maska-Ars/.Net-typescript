using App.Models;
using Microsoft.EntityFrameworkCore;
namespace App.DataAccess;

/// <summary>
/// Контекст Entity Framework Core для доступа к базе данных приложения.
/// Использует PostgreSQL в качестве системы управления базами данных.
/// </summary>
public class AppDbContext(IConfiguration configuration) : DbContext
{
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// DbSet для сущности Artist.
    /// </summary>
    public DbSet<Artist> Artists => Set<Artist>();

    /// <summary>
    /// DbSet для сущности Release.
    /// </summary>
    public DbSet<Release> Releases => Set<Release>();

    /// <summary>
    /// DbSet для сущности Record.
    /// </summary>
    public DbSet<Record> Records => Set<Record>();

    /// <summary>
    /// Метод для настройки подключения к базе данных.
    /// Использует строку подключения из конфигурации приложения.
    /// </summary>
    /// <param name="optionsBuilder">Объект DbContextOptionsBuilder для настройки параметров.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
    }
}