using SQLite;
using System.Collections.Generic;
using System.IO;
using LarreaPaulEval3.Models;

namespace LarreaPaulEval3.Services
{
    public class MovieService
    {
        private readonly SQLiteConnection _baseDatos;

        public MovieService()
        {
            var rutaBd = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "peliculas.db");
            _baseDatos = new SQLiteConnection(rutaBd);
            _baseDatos.CreateTable<Pelicula>();
        }

        public void AgregarPelicula(Pelicula pelicula)
        {
            _baseDatos.Insert(pelicula);
        }

        public List<Pelicula> GetPeliculas()
        {
            return _baseDatos.Table<Pelicula>().ToList();
        }

        public void EliminarPeliculas()
        {
            _baseDatos.DeleteAll<Pelicula>();
        }
    }
}
