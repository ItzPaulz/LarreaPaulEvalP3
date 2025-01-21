using System.Windows.Input;
using LarreaPaulEval3.Models;
using LarreaPaulEval3.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LarreaPaulEval3.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private string _nombrePelicula;
        private string _mensajeResultado;
        private readonly MovieAPIService _apiService;
        private readonly MovieDatabaseService _databaseService;

        public event PropertyChangedEventHandler PropertyChanged;

        public string NombrePelicula
        {
            get => _nombrePelicula;
            set
            {
                if (_nombrePelicula != value)
                {
                    _nombrePelicula = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MensajeResultado
        {
            get => _mensajeResultado;
            set
            {
                if (_mensajeResultado != value)
                {
                    _mensajeResultado = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ComandoBuscar { get; }
        public ICommand ComandoLimpiar { get; }

        public SearchViewModel()
        {
            _apiService = new MovieAPIService();
            _databaseService = new MovieDatabaseService(App.DatabasePath);

            ComandoBuscar = new Command(async () => await BuscarPeliculaAsync());
            ComandoLimpiar = new Command(LimpiarCampos);
        }

        private async Task BuscarPeliculaAsync()
        {
            if (string.IsNullOrWhiteSpace(NombrePelicula))
            {
                MensajeResultado = "Por favor, ingrese un nombre de película.";
                return;
            }

            var pelicula = await _apiService.ObtenerPeliculaAsync(NombrePelicula);
            if (pelicula != null)
            {
                await _databaseService.GuardarPeliculaAsync(pelicula);
                MensajeResultado = $"¡Película guardada! Título: {pelicula.Titulo}";
            }
            else
            {
                MensajeResultado = "No se encontró ninguna película con ese nombre.";
            }
        }

        private void LimpiarCampos()
        {
            NombrePelicula = string.Empty;
            MensajeResultado = string.Empty;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}