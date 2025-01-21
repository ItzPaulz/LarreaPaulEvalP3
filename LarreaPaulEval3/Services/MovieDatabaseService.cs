using LarreaPaulEval3.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LarreaPaulEval3.Services
{
    public class MovieDatabaseService


    {
        private readonly SQLiteAsyncConnection _baseDatos;

        public MovieDatabaseService(string rutaBd)
        {
            _baseDatos = new SQLiteAsyncConnection(rutaBd);
            _baseDatos.CreateTableAsync<Pelicula>().Wait(); 
        }

        public Task<List<Pelicula>> ObtenerPeliculasAsync()
        {
            return _baseDatos.Table<Pelicula>().ToListAsync();
        }

        public Task<int> GuardarPeliculaAsync(Pelicula pelicula)
        {
            return _baseDatos.InsertAsync(pelicula);
        }

        public Task EliminarPeliculasAsync()
        {
            return _baseDatos.DeleteAllAsync<Pelicula>();
        }
    }
}
