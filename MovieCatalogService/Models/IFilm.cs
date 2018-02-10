using System;

namespace MovieCatalogService
{
    interface IFilm
    {
        Guid Id { get; set; }
        string Title { get; set; }
        string Director { get; set; }
        int Year { get; set; }
    }
}
