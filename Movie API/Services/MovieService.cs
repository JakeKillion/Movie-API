using Movie_API.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_API.Services
{
    public class MovieService
    {
        public MovieService()
        {
        }

        private MetadataService metadataService = null;
        private MetadataService MetadataService
        {
            get
            {
                if (metadataService == null)
                {
                    metadataService = new MetadataService();
                }
                return metadataService;
            }
        }

        private MovieStatService movieStatService = null;
        private MovieStatService MovieStatService
        {
            get
            {
                if (movieStatService == null)
                {
                    movieStatService = new MovieStatService();
                }
                return movieStatService;
            }
        }

        private List<Movie> movies = null;
        private List<Movie> Movies
        {
            get
            {
                if (movies == null)
                {
                    movies = new List<Movie>();
                    Dictionary<int, List<Metadata>> metadataByMovieId = MetadataService.GetAllMetadataDictionary();
                    IEnumerable<int> movieIds = metadataByMovieId.Select(x => x.Key);
                    foreach (int movieId in movieIds)
                    {
                        List<Metadata> metadata = metadataByMovieId[movieId];
                        List<MovieStat> movieStats = MovieStatService.GetMovieStatByMovieId(movieId);

                        var movie = new Movie();
                        movie.MovieId = movieId;
                        movie.MovieStats = movieStats;
                        movie.Metadata = metadata;
                        movies.Add(movie);
                    }
                }
                return movies;
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            return Movies;
        }
    }
}
