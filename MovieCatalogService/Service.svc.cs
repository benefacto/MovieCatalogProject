﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace MovieCatalogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    // TO-DO: Enable CORS so front end can access as per https://enable-cors.org/server_wcf.html
    public class Service : IService
    {
        private string FilePath { get; set; }

        public Service()
        {
            FilePath = HostingEnvironment.MapPath(@"~\Data\MovieCatalog.json");
        }
        public string GetMovies()
        {
            IEnumerable<Movie> movies;
            try
            {
                movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>
                    (File.ReadAllText(FilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(movies);
        }

        public string UpdateMovie(string movieToUpdate)
        {
            string outcome = "failure";
            List<Movie> movies;
            Movie newMovie = JsonConvert.DeserializeObject<Movie>(movieToUpdate);
            Movie oldMovie;
            try
            {
                movies = (JsonConvert.DeserializeObject<IEnumerable<Movie>>
                    (File.ReadAllText(FilePath))).ToList<Movie>();
                oldMovie = movies.Single(m => m.Id == newMovie.Id);
                movies.Add(newMovie);
                movies.Remove(oldMovie);
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(movies));

                outcome = "success";
            }
            catch (Exception ex)
            {
                outcome += " " + ex.Message;
            }
            return outcome;
        }
    }
}
