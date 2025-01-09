using App.Models;
using Microsoft.EntityFrameworkCore;
namespace App.DataAccess;

public class AppDbContext: DbContext{
    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration){
        _configuration = configuration;

    }
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Release> Releases => Set<Release>();
    public DbSet<Record> Records => Set<Record>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
    }

    

}