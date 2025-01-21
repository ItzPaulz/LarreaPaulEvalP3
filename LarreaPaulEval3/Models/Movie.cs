using SQLite;
namespace LarreaPaulEval3.Models;
public class Pelicula
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; } 
    public string Titulo { get; set; }
    public string Genero { get; set; } 
    public string ActorPrincipal { get; set; } 
    public string Premios { get; set; } 
    public string SitioWeb { get; set; } 
    public string Usuario { get; set; } = "PLarrea"; 
}