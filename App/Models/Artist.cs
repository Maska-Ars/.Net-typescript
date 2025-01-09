using Microsoft.AspNetCore.SignalR;

namespace App.Models;

public class Artist{
    public Artist(string name, string country){
        Name = name;
        Country = country;

    }
    public Guid Id {get; set;}
    public string Name {get; set;}
    public string Country {get; set;}


}