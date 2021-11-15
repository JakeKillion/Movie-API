using System.Collections.Generic;

namespace Movie_API.Database.Metadata
{
    public class MetadataCUD
    {
        private List<Models.Metadata> Database = new List<Models.Metadata>();

        public MetadataCUD()
        {
        }

        public void Save(Models.Metadata movie)
        {
            Database.Add(movie);
        }
    }
}