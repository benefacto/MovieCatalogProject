using System;

namespace MovieCatalogService
{
    public class Movie : IFilm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
    }
}