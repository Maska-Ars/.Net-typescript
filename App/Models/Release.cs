using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

public class Release
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Image { get; set; }

    [Required]
    public Guid ReleaseId { get; set; }

    [Required]
    public Guid ArtistId { get; set; }

    [ForeignKey("ArtistId")]
    public Artist? Artist { get; set; } 

    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
}