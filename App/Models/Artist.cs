using Microsoft.AspNetCore.SignalR;

namespace App.Models;

public class Artist(string name, string country)
{
    public Guid Id {get; set;}

    public string Name { get; set; } = name;

    public string Country { get; set; } = country;
}