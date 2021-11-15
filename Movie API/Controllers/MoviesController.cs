using Microsoft.AspNetCore.Mvc;
using Movie_API.Models;
using Movie_API.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Movie_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public MoviesController()
        {
        }

        private MovieService movieService;
        public MovieService MovieService
        {
            get
            {
                if (movieService == null)
                {
                    movieService = new MovieService();
                }
                return movieService;
            }
        }

        // /movies/stats
        [HttpGet]
        [Route("stats")]
        public string Stats()
        {
            IEnumerable<Movie> allMovies = MovieService.GetAll();
            allMovies = allMovies.OrderByDescending(x => x.Watches).ThenByDescending(x => x.ReleaseYear).ToList();
            string serializedStats = JsonConvert.SerializeObject(allMovies);
            return serializedStats;
        }
    }
}
