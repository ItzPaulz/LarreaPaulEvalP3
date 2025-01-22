using System.Collections.ObjectModel;
using System.Net.Http.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui;
using LarreaPaulEval3.Services;
using LarreaPaulEval3.Models;

namespace LarreaPaulEval3.ViewModels;

public partial class SearchViewModel : ObservableObject
{
    private readonly MovieService _movieService;

    [ObservableProperty]
    private string tituloPelicula;

    [ObservableProperty]
    private string mensajeResultado;

    public ObservableCollection<Pelicula> Peliculas { get; } = new();

    public SearchViewModel()
    {
        _movieService = new MovieService();
    }

    [RelayCommand]
    public async Task BuscarPeliculaAsync()
    {
        try
        {
            using var httpClient = new HttpClient();
            string url = $"https://www.freetestapi.com/api/v1/movies?search={TituloPelicula}";
            var response = await httpClient.GetFromJsonAsync<List<ApiResponse>>(url);

            if (response != null && response.Count > 0)
            {
                var movie = response[0];
                var actorPrincipal = movie.ActorPrincipal ?? "Desconocido";
                var genero = string.IsNullOrEmpty(movie.Genero) ? "Desconocido" : movie.Genero;

                MensajeResultado = $"Titulo: {movie.Titulo}\nGenero: {genero}\nActor Principal: {actorPrincipal}\nPremios: {movie.Premios}\nSitio Web: {movie.SitioWeb}\nUsuario: {movie.Usuario}";

                var nuevaPelicula = new Pelicula
                {
                    Titulo = movie.Titulo,
                    Genero = genero,
                    ActorPrincipal = actorPrincipal,
                    Premios = movie.Premios,
                    SitioWeb = movie.SitioWeb,
                    Usuario = movie.Usuario
                };

                // Agregar la nueva película a la base de datos
                _movieService.AgregarPelicula(nuevaPelicula);

                // Actualizar la lista de películas
                Peliculas.Add(nuevaPelicula);
            }
            else
            {
                MensajeResultado = "No se encontró la película.";
            }
        }
        catch (Exception e)
        {
            MensajeResultado = $"Error: {e.Message}";
        }
    }

    [RelayCommand]
    public void EliminarPeliculas()
    {
        TituloPelicula = string.Empty;
        MensajeResultado = string.Empty;
    }

    public class ApiResponse
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string ActorPrincipal { get; set; }
        public string Premios { get; set; }
        public string SitioWeb { get; set; }
        public string Usuario { get; set; } = "PLarrea";
    }
}
