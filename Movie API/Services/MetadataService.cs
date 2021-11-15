using Movie_API.Database.Metadata;
using Movie_API.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Movie_API.Services
{
    public class MetadataService
    {
        public MetadataService()
        {
        }

        const string metadataFilePath = @"metadata.csv"; // CHANGE to the location of metadata.csv on your local machine e.g. C:\Movie API\metadata.csv

        private MetadataCUD metadataCUD = null;
        private MetadataCUD MetadataCUD
        {
            get
            {
                if (metadataCUD == null)
                {
                    metadataCUD = new MetadataCUD();
                }
                return metadataCUD;
            }
        }

        private ConcurrentDictionary<int, List<Metadata>> metadataByMovieId = null;
        private ConcurrentDictionary<int, List<Metadata>> MetadataByMovieId
        {
            get
            {
                if (metadataByMovieId == null)
                {
                    metadataByMovieId = new ConcurrentDictionary<int, List<Metadata>>();
                    string[] lines = System.IO.File.ReadAllLines(metadataFilePath);
                    foreach (string line in lines.Skip(1))
                    {
                        int counter = 1;
                        var metadata = new Metadata();
                        var columns = line.Split(',');
                        if (columns.Length == 6)
                        {
                            foreach (string column in columns)
                            {
                                if (column.Contains('"') == false)
                                {
                                    switch (counter)
                                    {
                                        case 1:
                                            if (int.TryParse(column, out int id))
                                            {
                                                metadata.Id = id;
                                                counter++;
                                            }
                                            break;
                                        case 2:
                                            if (int.TryParse(column, out int movieId))
                                            {
                                                metadata.MovieId = movieId;
                                                counter++;
                                            }
                                            break;
                                        case 3:
                                            metadata.Title = column;
                                            counter++;
                                            break;
                                        case 4:
                                            metadata.Language = column;
                                            counter++;
                                            break;
                                        case 5:
                                            metadata.Duration = column;
                                            counter++;
                                            break;
                                        case 6:
                                            if (int.TryParse(column, out int releaseYear))
                                            {
                                                metadata.ReleaseYear = releaseYear;
                                                counter++;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        if (metadata.isValid() == true)
                        {
                            if (metadataByMovieId.TryAdd(metadata.MovieId, new List<Metadata>() { metadata }) == false)
                            {
                                metadataByMovieId[metadata.MovieId].Add(metadata);
                            }
                        }
                    }
                }
                return metadataByMovieId;
            }
        }

        public List<Metadata> GetMetadataByMovieId(int movieId)
        {
            if (MetadataByMovieId.ContainsKey(movieId))
            {
                return MetadataByMovieId[movieId];
            }
            return null;
        }

        public Dictionary<int, List<Metadata>> GetAllMetadataDictionary()
        {
            return MetadataByMovieId.ToDictionary(x => x.Key, x => x.Value);
        }

        public void Save(Metadata metadata)
        {
            MetadataCUD.Save(metadata);
        }
    }
}