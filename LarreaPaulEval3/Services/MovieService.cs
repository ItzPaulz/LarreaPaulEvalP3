using SQLite;
using LarreaPaulEval3.Models;
using System.IO;

namespace LarreaPaulEval3.Services
{
    public class MovieService
    {
        private readonly SQLiteConnection _database;

        public MovieService()
        {
            
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "plarrea_movies.db");
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<Pelicula>();
        }


        public void AddMovie(Pelicula pelicula)
        {
            _database.Insert(pelicula);
        }

        public List<Pelicula> GetMovies()
        {
            return _database.Table<Pelicula>().ToList();
        }

        public void DeleteAllMovies()
        {
            _database.DeleteAll<Pelicula>();
        }
    }
}
