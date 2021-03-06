﻿using System;

namespace MovieCatalogService
{
    public class Movie : IFilm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public int RunningTime { get; set; }

        public override string ToString()
        {
            return Id + " " + Title + " " + Director + " " + Year + " " + RunningTime;
        }
    }
}