using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movie_API.Models
{
    public class Movie
    {
        public Movie()
        {
        }

        public int MovieId { get; set; }

        private string title = null;
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(title) == true && LatestRelease != null)
                {
                    title = LatestRelease.Title;
                }
                return title;
            }
        }

        private int averageWatchDurationS = 0;
        public int AverageWatchDurationS
        {
            get
            {
                if (averageWatchDurationS == 0 && MovieStats != null)
                {
                    IEnumerable<int> watchDurationMsList = MovieStats.Select(x => x.WatchDurationMs);
                    Int64 totalWatchDurationMs = 0;
                    int counter = 0;
                    int test = watchDurationMsList.Count();
                    foreach (int watchDurationMs in watchDurationMsList)
                    {
                        counter++;
                        totalWatchDurationMs += watchDurationMs;
                    }
                    averageWatchDurationS = (int)(totalWatchDurationMs / 1000);
                }
                return watches;
            }
        }

        private int watches = 0;
        public int Watches
        {
            get
            {
                if (watches == 0 && MovieStats != null)
                {
                    watches = MovieStats.Count;
                }
                return watches;
            }
        }

        private int releaseYear = 0;
        public int ReleaseYear
        {
            get
            {
                if (releaseYear == 0 && LatestRelease != null)
                {
                    releaseYear = LatestRelease.ReleaseYear;
                }
                return releaseYear;
            }
        }

        private List<Metadata> metadata = null;
        [JsonIgnore]
        public List<Metadata> Metadata
        {
            get
            {
                return metadata;
            }
            set
            {
                metadata = value;
            }
        }

        private List<MovieStat> movieStats = null;
        [JsonIgnore]
        public List<MovieStat> MovieStats
        {
            get
            {
                return movieStats;
            }
            set
            {
                movieStats = value;
            }
        }

        private Metadata latestRelease;
        private Metadata LatestRelease
        {
            get
            {
                if (latestRelease == null)
                {
                    latestRelease = Metadata.OrderByDescending(x => x.ReleaseYear).FirstOrDefault();
                }
                return latestRelease;
            }
        }
    }
}