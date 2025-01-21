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
                var actorPrincipal = movie.Actors != null && movie.Actors.Count > 0 ? movie.Actors[0] : "Desconocido";
                var genero = movie.Genre != null && movie.Genre.Count > 0 ? string.Join(", ", movie.Genre) : "Desconocido";

                MensajeResultado = $"Titulo: {movie.Title}\nGenero: {genero}\nActor Principal: {actorPrincipal}\nPremios: {movie.Awards}\nSitio Web: {movie.Website}\nUsuario: PLarrea";

                var nuevaPelicula = new Pelicula
                {
                    Titulo = movie.Title,
                    Genero = genero,
                    ActorPrincipal = actorPrincipal,
                    Premios = movie.Awards,
                    SitioWeb = movie.Website,
                    Usuario = "PLarrea"
                };

                _movieService.AddMovie(nuevaPelicula);
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
        public string Title { get; set; }
        public List<string> Genre { get; set; }
        public List<string> Actors { get; set; }
        public string Awards { get; set; }
        public string Website { get; set; }
    }
}
