using Newtonsoft.Json;

namespace Movie_API.Models
{
    public class MovieStat
    {
        public MovieStat()
        {
        }

        public int MovieId { get; set; }
        [JsonIgnore]
        public int WatchDurationMs { get; set; }
    }
}
