using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

public class Record
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int DurationSeconds { get; set; }

    [Required]
    public Guid ReleaseId { get; set; }

    [ForeignKey("ReleaseId")]
    public Release? Release { get; set; }
}
