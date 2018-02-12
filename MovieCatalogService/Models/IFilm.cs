using Newtonsoft.Json;
using System;

namespace MovieCatalogService
{
    interface IFilm
    {
        [JsonProperty("id")]
        Guid Id { get; set; }
        [JsonProperty("title")]
        string Title { get; set; }
        [JsonProperty("director")]
        string Director { get; set; }
        [JsonProperty("year")]
        int Year { get; set; }
    }
}
