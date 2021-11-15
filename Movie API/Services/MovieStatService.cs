using Movie_API.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Movie_API.Services
{
    public class MovieStatService
    {
        public MovieStatService()
        {
        }

        const string statsFilePath = @"stats.csv"; // CHANGE to the location of stats.csv on your local machine e.g. C:\Movie API\stats.csv

        private ConcurrentDictionary<int, List<MovieStat>> movieStatsByMovieId = null;
        public ConcurrentDictionary<int, List<MovieStat>> MovieStatsByMovieId
        {
            get
            {
                if (movieStatsByMovieId == null)
                {
                    movieStatsByMovieId = new ConcurrentDictionary<int, List<MovieStat>>();
                    string[] lines = System.IO.File.ReadAllLines(statsFilePath);
                    foreach (string line in lines.Skip(1))
                    {
                        int counter = 1;
                        var movieStat = new MovieStat();
                        foreach (string column in line.Split(','))
                        {
                            switch (counter)
                            {
                                case 1:
                                    if (int.TryParse(column, out int movieId))
                                    {
                                        movieStat.MovieId = movieId;
                                        counter++;
                                    }
                                    break;
                                case 2:
                                    if (int.TryParse(column, out int watchDurationMs))
                                    {
                                        movieStat.WatchDurationMs = watchDurationMs;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (movieStatsByMovieId.TryAdd(movieStat.MovieId, new List<MovieStat>() { movieStat }) == false)
                        {
                            movieStatsByMovieId[movieStat.MovieId].Add(movieStat);
                        }
                    }
                }
                return movieStatsByMovieId;
            }
        }

        public List<MovieStat> GetMovieStatByMovieId(int movieId)
        {
            if (MovieStatsByMovieId.ContainsKey(movieId))
            {
                return MovieStatsByMovieId[movieId];
            }
            return null;
        }
    }
}