using Newtonsoft.Json;

namespace Movie_API.Models
{
    public class Metadata
    {
        public Metadata()
        {
        }

        [JsonIgnore]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }

        public bool isValid()
        {
            if (string.IsNullOrEmpty(Title) == true)
            {
                return false;
            }
            if (string.IsNullOrEmpty(Language) == true)
            {
                return false;
            }
            if (string.IsNullOrEmpty(Duration) == true)
            {
                return false;
            }
            if (ReleaseYear == 0)
            {
                return false;
            }
            return true;
        }
    }
}
