using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LarreaPaulEval3.Models;
using LarreaPaulEval3.Services;
using System.Linq;


namespace LarreaPaulEval3.ViewModels
{
    public partial class MovieListViewModel : ObservableObject
    {
        private readonly MovieService _movieService;

        [ObservableProperty]
        private ObservableCollection<string> movies;

        public MovieListViewModel()
        {
            _movieService = new MovieService();
            LoadMovies();
        }

        private void LoadMovies()
        {
            var listaMovies = _movieService.GetPeliculas();
            Movies = new ObservableCollection<string>(
                listaMovies.Select(m =>
                    $"Titulo: {m.Titulo}\nGenero: {m.Genero}\nActor Principal: {m.ActorPrincipal}\nPremios: {m.Premios}\nSitio Web: {m.SitioWeb}\nUsuario: {m.Usuario}"
                )
            );
        }
    }
}
