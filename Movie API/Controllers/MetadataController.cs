using Microsoft.AspNetCore.Mvc;
using Movie_API.Models;
using Movie_API.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        public MetadataController()
        {
        }

        private MetadataService metadataService;
        public MetadataService MetadataService
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

        [HttpGet]
        [Route("start")]
        public IActionResult Start()
        {
            return Ok("API Online...");
        }

        // metadata/movieId
        [HttpGet]
        public string Get(string movieId)
        {
            try
            {
                if (int.TryParse(movieId, out int newMovieId) == false)
                {
                    throw new Exception("MovieId provided is invalid.");
                }
                List<Metadata> movieMetadata = MetadataService.GetMetadataByMovieId(newMovieId);
                if (movieMetadata == null)
                {
                    Response.StatusCode = 404;
                    throw new Exception($"Movie Metadata not found for MovieId '{movieId}'.");
                }
                movieMetadata = movieMetadata.OrderBy(x => x.Language).ThenByDescending(x => x.Id).ToList();
                return JsonConvert.SerializeObject(movieMetadata);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        // /metadata
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var metadata = new Metadata();
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string sMetadata = await reader.ReadToEndAsync();
                metadata = JsonConvert.DeserializeObject<Metadata>(sMetadata);
            }
            MetadataService.Save(metadata);
            return Ok("Metadata saved");
        }
    }
}
