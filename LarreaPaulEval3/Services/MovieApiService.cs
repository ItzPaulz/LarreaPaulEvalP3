using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using LarreaPaulEval3.Models;

namespace LarreaPaulEval3.Services
{
    public class MovieAPIService
    {
        private static readonly HttpClient cliente = new HttpClient();

        public async Task<Pelicula> ObtenerPeliculaAsync(string nombrePelicula)
        {
            var respuesta = await cliente.GetStringAsync($"https://freetestapi.com/api/v1/movies?search={nombrePelicula}&limit=1");
            var respuestaApi = JsonConvert.DeserializeObject<RespuestaApiPelicula>(respuesta);

            if (respuestaApi?.Peliculas?.Count > 0)
            {
                var pelicula = respuestaApi.Peliculas[0];
                return new Pelicula
                {
                    Titulo = pelicula.Titulo,
                    Genero = pelicula.Genero,
                    ActorPrincipal = pelicula.ActorPrincipal,
                    Premios = pelicula.Premios,
                    SitioWeb = pelicula.SitioWeb,
                    Usuario = "PLarrea"
                };
            }
            return null;
        }

        public class RespuestaApiPelicula
        {
            public List<Pelicula> Peliculas { get; set; }
        }

    }
}
